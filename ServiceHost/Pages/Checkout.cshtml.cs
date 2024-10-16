﻿using System.Globalization;
using _0_framework.Application;
using _0_framework.Application.ZarinPal;
using _01_LampshadeQuery.Contracts;
using _01_LampshadeQuery.Contracts.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nancy.Json;
using ShopManagement.Application.Contracts.Order;

namespace ServiceHost.Pages
{
    [Authorize]
    public class CheckoutModel : PageModel
    {
        public Cart Cart;
        public const string CookieName = "cart-items";
        private readonly ICartService _cartService;
        private readonly IProductQuery _productQuery;
        private readonly IZarinPalFactory _zarinPalFactory;
        private readonly IOrderApplication _orderApplication;
        private readonly ICartCalculatorService _cartCalculatorService;

        public CheckoutModel(ICartCalculatorService cartCalculatorService, ICartService cartService,
            IProductQuery productQuery, IOrderApplication orderApplication, IAuthHelper authHelper, IZarinPalFactory zarinPalFactory)
        {
            Cart = new Cart();
            _cartCalculatorService = cartCalculatorService;
            _cartService = cartService;
            _productQuery = productQuery;
            _orderApplication = orderApplication;
            _zarinPalFactory = zarinPalFactory;
        }
        

        public void OnGet()
        {
            var serializer = new JavaScriptSerializer();
            var value = Request.Cookies[CookieName];
            var cartItems = serializer.Deserialize<List<CartItem>>(value);
            foreach (var item in cartItems)
                item.CalculateTotalItemPrice();

            Cart = _cartCalculatorService.ComputeCart(cartItems);
            _cartService.Set(Cart);
        }

        public IActionResult OnPostPay(int paymentMethod)
        {
            var paymentResult = new PaymentResult();
    
            var cart = _cartService.Get();
            cart.SetPaymentMethod(paymentMethod);

            var result = _productQuery.CheckInventoryStatus(cart.Items);
            if (result.Any(x => !x.IsInStock))
                return RedirectToPage("/Cart");

            var orderId = _orderApplication.PlaceOrder(cart);
            if (paymentMethod == 1)
            {
                var paymentResponse = _zarinPalFactory.CreatePaymentRequest(
                    cart.PayAmount.ToString(CultureInfo.InvariantCulture), "", "",
                    "خرید از درگاه لوازم خانگی و دکوری", orderId).Result; // await در اینجا نیاز است.

                if (paymentResponse == null)
                    return RedirectToPage("/Checkout",
                        paymentResult.Failed("خطایی رخ داد"));

                return Redirect(
                    $"https://{_zarinPalFactory.Prefix}.zarinpal.com/pg/StartPay/{paymentResponse.Data.Authority}");
            }

            return RedirectToPage("/PaymentResult",
                paymentResult.Succeeded(
                    "سفارش شما با موفقیت ثبت شد. پس از تماس کارشناسان ما و پرداخت وجه، سفارش ارسال خواهد شد.", null));
        }

        
        public async Task<IActionResult> OnGetCallBack([FromQuery] string authority, [FromQuery] string status, [FromQuery] long oId)
        {
            var result = new PaymentResult();
    
            try
            {
                var orderAmount = _orderApplication.GetAmountBy(oId);
                var verificationResponse = await _zarinPalFactory.CreateVerificationRequest(authority, orderAmount.ToString(CultureInfo.InvariantCulture));

                if (status == "OK" && verificationResponse.data.code >= 100)
                {
                    var issueTrackingNo = _orderApplication.PaymentSucceeded(oId, verificationResponse.data.ref_id);
                    Response.Cookies.Delete("cart-items");
                    result = result.Succeeded("پرداخت با موفقیت انجام شد.", issueTrackingNo);
                    return RedirectToPage("/PaymentResult", result);
                }

                result = result.Failed("پرداخت با موفقیت انجام نشد. درصورت کسر وجه از حساب، مبلغ تا 24 ساعت دیگر به حساب شما بازگردانده خواهد شد.");
                return RedirectToPage("/PaymentResult", result);
            }
            catch (Exception ex)
            {
                // هدایت به صفحه خطا یا نمایش پیام مناسب به کاربر
                result = result.Failed("خطایی در پردازش پرداخت رخ داد: " + ex.Message);
                return RedirectToPage("/PaymentResult", result);
            }
        }


    }
}