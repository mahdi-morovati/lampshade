### ProductCategory CRUD

* create Domain Class Library (ShopManagement.Domain)
  create ProductCategory class
  create IProductCategoryRepository


* Create 0_framework class library

1- create EntityBase class for models.(Domain models implement it)

* create ShopManagement.Application.Contracts
  1- ProductCategory directory create CreateProductCategory, EditProductCategory classes
  2- create ProductCategorySearchModel, ProductCategoryViewModel
  3 - create IProductCategoryApplication interface

* create ProductCategoryApplication in ShopManagement.Application
*

**Layer Orders**
UI => call application layer
Application call repository from infrastructure
Application call Domain

* create ProductCategoryApplication in ShopManagement.Application

* create ShopManagement.Infrastructure.EFCore
    * creating mappings, repositories, DbContext
    * install Microsoft.EntityFrameworkCore/8.0.7
    * install Microsoft.EntityFrameworkCore.SqlServer/8.0.7
    * install Microsoft.EntityFrameworkCore.Tools/8.0.7
    * create ProductCategoryMapping
    * create ProductCategoryRepository

* create IRepository in 0_framework/Domain
* create Repository in 0_framework/Infrastructure
* implement IProductCategoryRepository and ProductCategoryRepository from IRepository

* کانفیگ کردن اینترفیس ها و پیاده سازیشون
* so create ShopManagement.Configuration
* and create ShopManagementBootstrapper

* Create ServiceHost Project and inject ShopManagementBootstrapper to Program.cs


* create ServiceHost Project and add admin theme in Areas/Administration/
    * implement ServiceHost.Areas.Administration.Pages.Shop.ProductCategories.IndexModel.OnGet

* create Edit page

----------------------------------------------------------------
### Product CRUD
1- create Product model in ShopManagement.Domain (productAgg directory) and
create ctor for initial data for creating model
create edit for create editing data
2- create IProductRepository in ShopManagement.Domain
(productAgg)

3- in ShopManagement.Application.Contracts create Product folder
the application contract in fact is a place to define application interfaces. that is(that's) mean these classes are DTO
class.

* create CreateProduct class
* create EdtProduct class
* create ProductViewModel class
* create ProductSearchModel class

* create IProductApplication contains this methods

  OperationResult Create(CreateProduct command);
  OperationResult Edit(EditProduct command);
  EditProduct GetDetails(long id);
  List<ProductViewModel> Search(ProductSearchModel searchModel);

* define this methods in IProductRepository

  EditProduct GetDetails(long id);
  List<ProductViewModel> Search(ProductSearchModel searchModel);

* create ShopManagement.Application.ProductApplication that implements IProductApplication

    * create ShopManagement.Infrastructure.EFCore.Mapping.ProductMapping and define relationship with category.(define
      relationship in category too)
* define DbSet<Product> in ShopContext
* create ShopManagement.Infrastructure.EFCore.Repository.ProductRepository

* bind interfaces in ShopManagement.Configuration.ShopManagementBootstrapper

  services.AddTransient<IProductApplication, ProductApplication>();
  services.AddTransient<IProductRepository, ProductRepository>();

* create Products/ razor pages index in ServiceHost
* create: 
  * ServiceHost.Areas.Administration.Pages.Shop.Products.IndexModel.ProductCategories
  * ShopManagement.Domain.ProductCategoryAgg.IProductCategoryRepository.GetProductCategories
  * ShopManagement.Infrastructure.EFCore.Repository.ProductCategoryRepository.GetProductCategories
  * ShopManagement.Application.Contracts.ProductCategory.IProductCategoryApplication.GetProductCategories
  * ShopManagement.Application.ProductCategoryApplication.GetProductCategories
  * ServiceHost.Areas.Administration.Pages.Shop.Products.IndexModel.ProductCategories on OnGet method
  * create ProductAdded migration
  
* fix Edit.cshtml, Create.cshtml, ServiceHost.Areas.Administration.Pages.Shop.Products.IndexModel.OnGetEdit
----------------------------------------------------------------
### ProductPicture

----------------------------------------------------------------
### Slide
ما برای اینکه بتونیم اسلایدهارو بیاریم یک کلاس لایبرری جدا درست میکنیم. و دلیل اینکه مثل همیشه نمیریم تو اپلیکیشن اینه که ما میخوایم مستقیم کانتکس اینجکت کنیم و ممکنه بخوایم چنتا کانتکست اینجکت کنیم و نکته دیگه اینکه لاجیکی نداریم صرفا قراره کوئری زده بشه به دیتابیس و دیتا بیاد 
* create 01_LampshadeQuery Library class
* _01_LampshadeQuery.Contracts.Slide.SlideQueryModel
* create _01_LampshadeQuery.Contracts.Slide.ISlideQuery
* create _01_LampshadeQuery.Query.SlideQuery
* create ServiceHost/Pages/Shared/Components/Slide/Default.cshtml
* create ServiceHost.ViewComponents.SlideViewComponent
* wire up in ShopManagementBootstrapper

----------------------------------------------------------------
## Discount Management

1- create DiscountManagement.Domain class library

2- create DiscountManagement.Domain.CustomerDiscountAgg.CustomerDiscount (CustomerDiscount model, ICustomerDiscountRepository). create ctor and Edit methods for CustomerDiscount Domain.

3- create DiscountManagement.Application.Contract class library

4 - create DTOs in DiscountManagement.Application.Contract.CustomerDiscount (DefineCustomerDiscount, EditCustomerDiscount, CustomerDiscountViewModel, CustomerDiscountSearchModel, ICustomerDiscountApplication)

5- create DiscountManagement.Application (CustomerDiscountApplication : ICustomerDiscountApplication)

6- DiscountManagement.Infrastructure.EFCore (DiscountContext : DbContext). define DbSet<CustomerDiscount>

7- create DiscountManagement.Infrastructure.EFCore.Mapping.CustomerDiscountMapping

8- create DiscountManagement.Infrastructure.EFCore.Repository.CustomerDiscountRepository

9- implement DiscountContext

10- implement CustomDiscountRepository
in CustomDiscountRepository we need the product name for each discount. for do this we need inject the ShopContext, get products and loop on them and match with discount ProductId

11- implement DiscountManagement.Application.CustomerDiscountApplication

12- inject ServiceHost/Program.cs#L10-L10 DiscountManagementBootstrapper.Configure(builder.Services, connectionString);

13- create migration DiscountManagement.Infrastructure.EFCore.Migrations

14- create CustomerDiscount pages

**Colleague Discount**

Since this type of discount always exists, it is defined separately from the customer discount

----------------------------------------------------------------
### Inventory

* create InventoryManagement.Domain.InventoryAgg.Inventory
* create IInventoryRepository
* create InventoryOperation
* create Inventory DTOs, app interface
* create InventoryMapping
* create InventoryDbContext
* create repository
* create InventoryManagement.Infrastructure.Configuration.InventoryManagementBootstrapper
* create migration
* create InventoryManagement.Application.InventoryApplication. AddTransient in Inventory InventoryManagement.Infrastructure.Configuration.InventoryManagementBootstrapper.Configure
* add inventory pages in ServiceHost
* create OperationLog page
----------------------------------------------------------------

#### Project Ui

* create _01_LampshadeQuery.Contracts.Product.ProductQueryModel add as list to ProductCategoryQueryModel
* create _01_LampshadeQuery.Contracts.ProductCategory.IProductCategoryQuery.GetProductCategoriesWithProducts in implement in _01_LampshadeQuery.Query.ProductCategoryQuery.GetProductCategoriesWithProducts
* create ServiceHost/Pages/Shared/Components/ProductCategoryWithProduct/Default.cshtml
* create ServiceHost.ViewComponents.ProductCategoryWithProductViewComponent
* create LastsArrival View Component

----------------------------------------------------------------
#### File Upload

* change input to File type in ServiceHost/Areas/Administration/Pages/Shop/Products/Create.cshtml, Edit.cshtml
* change string to IFormFile type in ShopManagement.Application.Contracts.Product.CreateProduct.Picture
* create _0_framework.Application.IFileUploader, ServiceHost.FileUploader
* use FileUploader in ProductApplication
* create _0_framework.Application.MaxFileSizeAttribute for file size validation
* create _0_framework.Application.FileExtensionLimitationAttribute for file type 
  * use these validations in ShopManagement.Application.Contracts.Product.CreateProduct.Picture


----------------------------------------------------------------
### Form Validation 
* add jquery validation to layout 

      https://raw.githubusercontent.com/ssrsub/ssr/master/Clash.yml
* use $.validator.unobtrusive.parse(newForm); beacuse form is not exists in dom and gets with ajax. ServiceHost/wwwroot/AdminTheme/assets/js/site.js:19
* add validation attributes in commands (DTOs like CreateProduct, EditProduct, CreateProductCategory). 

----------------------------------------------------------------
### Product details page

* create Razor Page /home/mahdi/RiderProjects/Lampshade/ServiceHost/Pages/Product.cshtml, Product.cshtml.cs

----------------------------------------------------------------

## Authentication

* create Solution folder AccountManagement
* create Domain. create model AccountManagement.Domain.AccountAgg.Account, IAccountRepository
* create DTOs AccountManagement.Application.Contracts.Account.RegisterAccount, IAccountApplication, ...
* create AccountManagement.Domain.AccountAgg.IAccountRepository methods
* create AccountManagement.Infrastructure.EFCore
  * create AccountContext
  * create AccountMapping
  * create AccountRepository. (implement methods)
* create AccountManagement.Application.AccountApplication
  * extend methods
  * in ChangePassword method we must hash password
  * add IPasswordHasher, PasswordHasher, HashingOptions methods (from other project)
  * use PasswordHasher in AccountApplication.ChangePassword method
  * wire up in builder.Services.AddTransient<IPasswordHasher, PasswordHasher>();
    **wire up it is mean register in Program.cs (builder.Services.AddTransient)**
  * create AccountManagement.Configuration.AccountManagementBootstrapper and register in Program.cs
  * create migrations (Account, Role)
  * create pages (ServiceHost/Areas/Administration/Pages/Accounts/Account)
* create IAuthHelper (create it in _0_framework.Application because we want to use it always)
* create _0_framework.Application.AuthHelper
* add builder.Services.AddHttpContextAccessor(); in Program.cs to access _0_framework.Application.AuthHelper._contextAccessor (IHttpContextAccessor)
* register in Program.cs builder.Services.AddTransient<IAuthHelper, AuthHelper>();
* we must tell to app apply authentication check. add this code in Program.cs


    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.Strict;
    });
    builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o =>
    {
    o.LoginPath = new PathString("/Account");
    o.LogoutPath = new PathString("/Account");
    o.AccessDeniedPath = new PathString("/AccessDenied");
    });
  
* register app.UseAuthentication();, app.UseCookiePolicy(); in Program.cs. (if app.UseAuthorization(); is not exists we should add it)
* use IPageFilter. (create ServiceHost.SecurityPageFilter)
**in generic itself get instance from the class**

