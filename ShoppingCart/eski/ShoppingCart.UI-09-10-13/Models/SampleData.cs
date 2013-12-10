using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

using ShoppingCart.Data.Entity;
using ShoppingCart.Data.Concrete;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ShoppingCart.UI.Models
{
    public class SampleIdentityData : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext identityContext)
        {
            
        }
    }

    public class SampleData : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        //protected override void Seed(Identity.Models.ApplicationDbContext context)
        //{
        //    if (!context.Users.Any(u => u.UserName == "sallen"))
        //    {
        //        var store = new UserStore<ApplicationUser>(context);
        //        var manager = new UserManager<ApplicationUser>(store);
        //        var user = new ApplicationUser { UserName = "sallen" };

        //        manager.Create(user, "password");
        //    }
        //}

        protected override void Seed(ApplicationDbContext context)
        {

            #region Identity

            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            string name = "Admin";
            string password = "123456";
            string test = "test";

            //Create Role Test and User Test
            RoleManager.Create(new IdentityRole(test));
            UserManager.Create(new ApplicationUser() { UserName = test });

            //Create Role Admin if it does not exist
            if (!RoleManager.RoleExists(name))
            {
                var roleresult = RoleManager.Create(new IdentityRole(name));
            }

            //Create User=Admin with password=123456
            var user = new ApplicationUser();
            user.UserName = name;
            var adminresult = UserManager.Create(user, password);

            //Add User Admin to Role Admin
            if (adminresult.Succeeded)
            {
                var result = UserManager.AddToRole(user.Id, name);
            }
            //base.Seed(context);
            #endregion

            #region City
            var cities = new List<City>
            {
                new City{CityCode="01",CityName="Adana"},
                new City{CityCode="02",CityName="Adıyaman"},
                new City{CityCode="03",CityName="Afyonkarahisar"},
                new City{CityCode="04",CityName="Ağrı"},
                new City{CityCode="05",CityName="Amasya"},
                new City{CityCode="06",CityName="Ankara"},
                new City{CityCode="07",CityName="Antalya"},
                new City{CityCode="08",CityName="Artvin"},
                new City{CityCode="09",CityName="Aydın"},
                new City{CityCode="10",CityName="Balıkesir"},
                new City{CityCode="11",CityName="Bilecik"},
                new City{CityCode="12",CityName="Bingöl"},
                new City{CityCode="13",CityName="Bitlis"},
                new City{CityCode="14",CityName="Bolu"},
                new City{CityCode="15",CityName="Burdur"},
                new City{CityCode="16",CityName="Bursa"},
                new City{CityCode="17",CityName="Çanakkale"},
                new City{CityCode="18",CityName="Çankırı"},
                new City{CityCode="19",CityName="Çorum"},
                new City{CityCode="20",CityName="Denizli"},
                new City{CityCode="21",CityName="Diyarbakır"},
                new City{CityCode="22",CityName="Edirne"},
                new City{CityCode="23",CityName="Elazığ"},
                new City{CityCode="24",CityName="Erzincan"},
                new City{CityCode="25",CityName="Erzurum"},
                new City{CityCode="26",CityName="Eskişehir"},
                new City{CityCode="27",CityName="Gaziantep"},
                new City{CityCode="28",CityName="Giresun"},
                new City{CityCode="29",CityName="Gümüşhane"},
                new City{CityCode="30",CityName="Hakkari"},
                new City{CityCode="31",CityName="Hatay"},
                new City{CityCode="32",CityName="Isparta"},
                new City{CityCode="33",CityName="Mersin"},
                new City{CityCode="34",CityName="İstanbul"},
                new City{CityCode="35",CityName="İzmir"},
                new City{CityCode="36",CityName="Kars"},
                new City{CityCode="37",CityName="Kastamonu"},
                new City{CityCode="38",CityName="Kayseri"},
                new City{CityCode="39",CityName="Kırklareli"},
                new City{CityCode="40",CityName="Kırşehir"},
                new City{CityCode="41",CityName="Kocaeli"},
                new City{CityCode="42",CityName="Konya"},
                new City{CityCode="43",CityName="Kütahya"},
                new City{CityCode="44",CityName="Malatya"},
                new City{CityCode="45",CityName="Manisa"},
                new City{CityCode="46",CityName="Kahramanmaraş"},
                new City{CityCode="47",CityName="Mardin"},
                new City{CityCode="48",CityName="Muğla"},
                new City{CityCode="49",CityName="Muş"},
                new City{CityCode="50",CityName="Nevşehir"},
                new City{CityCode="51",CityName="Niğde"},
                new City{CityCode="52",CityName="Ordu"},
                new City{CityCode="53",CityName="Rize"},
                new City{CityCode="54",CityName="Sakarya"},
                new City{CityCode="55",CityName="Samsun"},
                new City{CityCode="56",CityName="Siirt"},
                new City{CityCode="57",CityName="Sinop"},
                new City{CityCode="58",CityName="Sivas"},
                new City{CityCode="59",CityName="Tekirdağ"},
                new City{CityCode="60",CityName="Tokat"},
                new City{CityCode="61",CityName="Trabzon"},
                new City{CityCode="62",CityName="Tunceli"},
                new City{CityCode="63",CityName="Şanlıurfa"},
                new City{CityCode="64",CityName="Uşak"},
                new City{CityCode="65",CityName="Van"},
                new City{CityCode="66",CityName="Yozgat"},
                new City{CityCode="67",CityName="Zonguldak"},
                new City{CityCode="68",CityName="Aksaray"},
                new City{CityCode="69",CityName="Bayburt"},
                new City{CityCode="70",CityName="Karaman"},
                new City{CityCode="71",CityName="Kırıkkale"},
                new City{CityCode="72",CityName="Batman"},
                new City{CityCode="73",CityName="Şırnak"},
                new City{CityCode="74",CityName="Bartın"},
                new City{CityCode="75",CityName="Ardahan"},
                new City{CityCode="76",CityName="Iğdır"},
                new City{CityCode="77",CityName="Yalova"},
                new City{CityCode="78",CityName="Karabük"},
                new City{CityCode="79",CityName="Kilis"},
                new City{CityCode="80",CityName="Osmaniye"},
                new City{CityCode="81",CityName="Düzce"}
            };
            #endregion

            #region Language
            var languages = new List<Language>
            {
                new Language{LanguageCode ="tr",LanguageName ="Türkçe",Default = true},
                new Language{LanguageCode ="en",LanguageName ="English"}
            };
            #endregion

            #region Category
            var categories = new List<Category>
            {
                new Category{ DateCreated =DateTime.Now,
                              DateModified = DateTime.Now,
                              CategoryURLText ="cuzdan",
                              CategoryTranslations = new List<CategoryTranslation>
                                  {new CategoryTranslation
                                    {Language = languages.Single(x=>x.LanguageCode =="tr"),CategoryName ="Cüzdan",CategoryDescription="Özel Tasarım Cüzdan"},
                                   new CategoryTranslation
                                    {Language = languages.Single(x=>x.LanguageCode =="en"),CategoryName ="Wallet",CategoryDescription="Special Design Wallet"}
                                  }
                            },
                new Category{ DateCreated =DateTime.Now,
                              DateModified = DateTime.Now,
                              CategoryURLText ="saat",
                              CategoryTranslations = new List<CategoryTranslation>
                                  {new CategoryTranslation
                                    {Language = languages.Single(x=>x.LanguageCode =="tr"),CategoryName ="Saat",CategoryDescription="Özel Tasarım Saat"},
                                   new CategoryTranslation
                                    {Language = languages.Single(x=>x.LanguageCode =="en"),CategoryName ="Watch",CategoryDescription="Special Design Watch"}
                                  }
                            },
                new Category{ DateCreated =DateTime.Now,
                    DateModified = DateTime.Now,
                    CategoryURLText ="ceket",
                    CategoryTranslations = new List<CategoryTranslation>
                        {new CategoryTranslation
                        {Language = languages.Single(x=>x.LanguageCode =="tr"),CategoryName ="Ceket",CategoryDescription="Özel Tasarım Ceket"},
                        new CategoryTranslation
                        {Language = languages.Single(x=>x.LanguageCode =="en"),CategoryName ="Coat",CategoryDescription="Special Design Coat"}
                        }
                },
                new Category{ DateCreated =DateTime.Now,
                    DateModified = DateTime.Now,
                    CategoryURLText ="t-shirt",
                    CategoryTranslations = new List<CategoryTranslation>
                        {new CategoryTranslation
                        {Language = languages.Single(x=>x.LanguageCode =="tr"),CategoryName ="T-Shirt",CategoryDescription="Özel Tasarım T-Shirt"},
                        new CategoryTranslation
                        {Language = languages.Single(x=>x.LanguageCode =="en"),CategoryName ="T-Shirt",CategoryDescription="Special Design T-Shirt"}
                        }
                }
            };




            categories.Add(new Category
            {
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                CategoryURLText="erkek-saat",
                Parent = categories.Single(x => x.CategoryTranslations.Single(y => y.Language.LanguageCode=="tr").CategoryName== "Saat"),
                Ancestors = new List<CategoryNode>{ new CategoryNode {Ancestor =
                                                    categories.Single(x => x.CategoryTranslations.Single(y => y.Language.LanguageCode=="tr").CategoryName== "Saat"),Separation=1 }},
                CategoryTranslations = new List<CategoryTranslation>
                                            {new CategoryTranslation
                                                {Language = languages.Single(x=>x.LanguageCode =="tr"),CategoryName ="Erkek Saat",CategoryDescription="Özel Tasarım Erkek Saat"},
                                            new CategoryTranslation
                                                {Language = languages.Single(x=>x.LanguageCode =="en"),CategoryName ="Men Watch",CategoryDescription="Special Design Men Watch"}
                                            }
            });


            categories.Add(new Category{DateCreated = DateTime.Now,
                                        DateModified = DateTime.Now,
                                        CategoryURLText = "kadin-saat",
                                        Parent = categories.Single(x => x.CategoryTranslations.Single(y => y.Language.LanguageCode == "tr").CategoryName == "Saat"),
                                        Ancestors = new List<CategoryNode>{ new CategoryNode {Ancestor =
                                                    categories.Single(x => x.CategoryTranslations.Single(y => y.Language.LanguageCode=="tr").CategoryName== "Saat"),Separation=1  }},
                                        CategoryTranslations = new List<CategoryTranslation>
                                            {new CategoryTranslation
                                                {Language = languages.Single(x=>x.LanguageCode =="tr"),CategoryName ="Kadın Saat",CategoryDescription="Özel Tasarım Kadın Saat"},
                                            new CategoryTranslation
                                                {Language = languages.Single(x=>x.LanguageCode =="en"),CategoryName ="Women Watch",CategoryDescription="Special Design Women Watch"}
                                            }
                                         });


            categories.Add(new Category
            {
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                CategoryURLText = "kadin-ceket",
                Parent = categories.Single(x => x.CategoryTranslations.Single(y => y.Language.LanguageCode == "tr").CategoryName == "Ceket"),
                Ancestors = new List<CategoryNode>{ new CategoryNode {Ancestor =
                                                    categories.Single(x => x.CategoryTranslations.Single(y => y.Language.LanguageCode=="tr").CategoryName== "Ceket"),Separation=1  }},
                CategoryTranslations = new List<CategoryTranslation>
                                            {new CategoryTranslation
                                                {Language = languages.Single(x=>x.LanguageCode =="tr"),CategoryName ="Kadın Ceket",CategoryDescription="Özel Tasarım Kadın Ceket"},
                                            new CategoryTranslation
                                                {Language = languages.Single(x=>x.LanguageCode =="en"),CategoryName ="Women Coat",CategoryDescription="Special Design Women Coat"}
                                            }
            });

            categories.Add(new Category
            {
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                CategoryURLText = "yazlik-kadin-ceket",
                Parent = categories.Single(x => x.CategoryTranslations.Single(y => y.Language.LanguageCode == "tr").CategoryName == "Kadın Ceket"),
                Ancestors = new List<CategoryNode>{ new CategoryNode {Ancestor =
                                                    categories.Single(x => x.CategoryTranslations.Single(y => y.Language.LanguageCode=="tr").CategoryName== "Ceket"),Separation=2  },
                                                    new CategoryNode {Ancestor =
                                                    categories.Single(x => x.CategoryTranslations.Single(y => y.Language.LanguageCode=="tr").CategoryName== "Kadın Ceket"),Separation=1  }
                },
                CategoryTranslations = new List<CategoryTranslation>
                                            {new CategoryTranslation
                                                {Language = languages.Single(x=>x.LanguageCode =="tr"),CategoryName ="Yazlık Kadın Ceket",CategoryDescription="Özel Tasarım Yazlık Kadın Ceket"},
                                            new CategoryTranslation
                                                {Language = languages.Single(x=>x.LanguageCode =="en"),CategoryName ="Summer Women Coat",CategoryDescription="Special Design Summer Women Coat"}
                                            }
            });

            #endregion

            #region Product
            var products = new List<Product>
            {
            #region 1
                new Product
                {
                    Category = categories.Single(x=> x.CategoryTranslations.Single(y=> y.Language.LanguageCode =="tr").CategoryName =="T-Shirt"),
                    ProductURLText="good-bad-ugly-t-shirt",
                    DateCreated=DateTime.Now,
                    DateModified = DateTime.Now,
                    DiscountedPrice = 40,
                    Enabled = true,
                    Inventory =5,
                    OriginalPrice = 40,
                    RelatedProducts = new List<ProductRelation>{},
                    ProductTranslations = new List<ProductTranslation>
                    {
                        new ProductTranslation
                            {Language = languages.Single(x=>x.LanguageCode =="tr"),ProductName ="Good, Bad, Ugly T-Shirt",ProductDescription="Good, Bad, Ugly T-Shirt"},
                        new ProductTranslation
                            {Language = languages.Single(x=>x.LanguageCode =="en"),ProductName ="Good, Bad, Ugly T-Shirt",ProductDescription="Good, Bad, Ugly T-Shirt"}
                                            
                    },
                    ProductImages = new List<ProductImage>
                    {
                        new ProductImage
                        {
                            DisplayOrder=1,
                            ProductImageMimeType ="image/jpeg",
                            ProductImagePath = "/Content/ProductImages/goodbadugly1.jpg"
                        }
                        //,
                        //new ProductImage
                        //{
                        //    DisplayOrder=2,
                        //    ProductImagePath = "path2"
                        //}
                    },
                    ProductVariants = new List<ProductVariant>
                        {
                            new ProductVariant
                            {
                                Enabled=true,
                                ProductVariantTranslations= new List<ProductVariantTranslation>
                                                                {
                                                                    new ProductVariantTranslation
                                                                    {
                                                                        Language =languages.Single(x=> x.LanguageCode =="tr"),
                                                                        ProductVariantDescription ="",
                                                                        ProductVariantName ="Renk"
                                                                    },
                                                                    new ProductVariantTranslation
                                                                    {
                                                                        Language =languages.Single(x=> x.LanguageCode =="en"),
                                                                        ProductVariantDescription ="",
                                                                        ProductVariantName ="Color",
                                                                    }
                                                                },
                                ProductVariantValues = new List<ProductVariantValue>
                                                                {
                                                                     new ProductVariantValue
                                                                     {
                                                                        Inventory = 3,
                                                                        Enabled = true,
                                                                        ProductVariantValueTranslations = new List<ProductVariantValueTranslation>
                                                                            {
                                                                                new ProductVariantValueTranslation
                                                                                {
                                                                                    Language =languages.Single(x=> x.LanguageCode =="tr"),
                                                                                    ProductVariantValueName ="Siyah"
                                                                                },
                                                                                new ProductVariantValueTranslation
                                                                                {
                                                                                    Language =languages.Single(x=> x.LanguageCode =="en"),
                                                                                    ProductVariantValueName ="Black"
                                                                                }
                                                                            }
                                                                     },
                                                                      new ProductVariantValue
                                                                     {
                                                                        Inventory = 4,
                                                                        Enabled = true,
                                                                        ProductVariantValueTranslations = new List<ProductVariantValueTranslation>
                                                                            {
                                                                                new ProductVariantValueTranslation
                                                                                {
                                                                                    Language =languages.Single(x=> x.LanguageCode =="tr"),
                                                                                    ProductVariantValueName ="Beyaz"
                                                                                },
                                                                                new ProductVariantValueTranslation
                                                                                {
                                                                                    Language =languages.Single(x=> x.LanguageCode =="en"),
                                                                                    ProductVariantValueName ="White"
                                                                                }
                                                                            }
                                                                     },
                                                                      new ProductVariantValue
                                                                     {
                                                                        Inventory = 2,
                                                                        Enabled = true,
                                                                        ProductVariantValueTranslations = new List<ProductVariantValueTranslation>
                                                                            {
                                                                                new ProductVariantValueTranslation
                                                                                {
                                                                                    Language =languages.Single(x=> x.LanguageCode =="tr"),
                                                                                    ProductVariantValueName ="Kırmızı"
                                                                                },
                                                                                new ProductVariantValueTranslation
                                                                                {
                                                                                    Language =languages.Single(x=> x.LanguageCode =="en"),
                                                                                    ProductVariantValueName ="Red"
                                                                                }
                                                                            }
                                                                     }
                                                                }
                            }
                        }
                },
                #endregion

            #region 2
                new Product
                {
                    Category = categories.Single(x=> x.CategoryTranslations.Single(y=> y.Language.LanguageCode =="tr").CategoryName =="Erkek Saat"),
                    ProductURLText="deri-erkek-saat",
                    DateCreated=DateTime.Now,
                    DateModified = DateTime.Now,
                    DiscountedPrice = 40,
                    Enabled = true,
                    Inventory =5,
                    OriginalPrice = 40,
                    RelatedProducts = new List<ProductRelation>{},
                    ProductTranslations = new List<ProductTranslation>
                    {
                        new ProductTranslation
                            {Language = languages.Single(x=>x.LanguageCode =="tr"),ProductName ="Deri Erkek Saat",ProductDescription="Özel Tasarım Erkek Saat Deri"},
                        new ProductTranslation
                            {Language = languages.Single(x=>x.LanguageCode =="en"),ProductName ="Leather Men Watch",ProductDescription="Special Design Men Watch Leather "}
                                            
                    },
                    ProductImages = new List<ProductImage>
                    {
                        new ProductImage
                        {
                            DisplayOrder=1,
                            ProductImageMimeType ="image/jpeg",
                            ProductImagePath = "/Content/ProductImages/derierkeksaat1.jpg"
                        },
                        new ProductImage
                        {
                            DisplayOrder=2,
                            ProductImageMimeType ="image/jpeg",
                            ProductImagePath = "/Content/ProductImages/derierkeksaat2.jpg"
                        }
                    },
                    ProductVariants = new List<ProductVariant>
                        {
                            new ProductVariant
                            {
                                Enabled=true,
                                ProductVariantTranslations= new List<ProductVariantTranslation>
                                                                {
                                                                    new ProductVariantTranslation
                                                                    {
                                                                        Language =languages.Single(x=> x.LanguageCode =="tr"),
                                                                        ProductVariantDescription ="",
                                                                        ProductVariantName ="Renk"
                                                                    },
                                                                    new ProductVariantTranslation
                                                                    {
                                                                        Language =languages.Single(x=> x.LanguageCode =="en"),
                                                                        ProductVariantDescription ="",
                                                                        ProductVariantName ="Color",
                                                                    }
                                                                },
                                ProductVariantValues = new List<ProductVariantValue>
                                                                {
                                                                     new ProductVariantValue
                                                                     {
                                                                        Inventory = 4,
                                                                        Enabled = true,
                                                                        ProductVariantValueTranslations = new List<ProductVariantValueTranslation>
                                                                            {
                                                                                new ProductVariantValueTranslation
                                                                                {
                                                                                    Language =languages.Single(x=> x.LanguageCode =="tr"),
                                                                                    ProductVariantValueName ="Siyah"
                                                                                },
                                                                                new ProductVariantValueTranslation
                                                                                {
                                                                                    Language =languages.Single(x=> x.LanguageCode =="en"),
                                                                                    ProductVariantValueName ="Black"
                                                                                }
                                                                            }
                                                                     },
                                                                      new ProductVariantValue
                                                                     {
                                                                        Inventory = 5,
                                                                        Enabled = true,
                                                                        ProductVariantValueTranslations = new List<ProductVariantValueTranslation>
                                                                            {
                                                                                new ProductVariantValueTranslation
                                                                                {
                                                                                    Language =languages.Single(x=> x.LanguageCode =="tr"),
                                                                                    ProductVariantValueName ="Beyaz"
                                                                                },
                                                                                new ProductVariantValueTranslation
                                                                                {
                                                                                    Language =languages.Single(x=> x.LanguageCode =="en"),
                                                                                    ProductVariantValueName ="White"
                                                                                }
                                                                            }
                                                                     },
                                                                      new ProductVariantValue
                                                                     {
                                                                        Inventory = 12,
                                                                        Enabled = true,
                                                                        ProductVariantValueTranslations = new List<ProductVariantValueTranslation>
                                                                            {
                                                                                new ProductVariantValueTranslation
                                                                                {
                                                                                    Language =languages.Single(x=> x.LanguageCode =="tr"),
                                                                                    ProductVariantValueName ="Kırmızı"
                                                                                },
                                                                                new ProductVariantValueTranslation
                                                                                {
                                                                                    Language =languages.Single(x=> x.LanguageCode =="en"),
                                                                                    ProductVariantValueName ="Red"
                                                                                }
                                                                            }
                                                                     }
                                                                }
                            }
                        }
                },
                #endregion 

            #region 3
                new Product
                {
                    Category = categories.Single(x=> x.CategoryTranslations.Single(y=> y.Language.LanguageCode =="tr").CategoryName =="Kadın Saat"),
                    ProductURLText="deri-kadin-saat",
                    DateCreated=DateTime.Now,
                    DateModified = DateTime.Now,
                    DiscountedPrice = 60,
                    Enabled = true,
                    Inventory =15,
                    OriginalPrice = 60,
                    RelatedProducts = new List<ProductRelation>{},
                    ProductTranslations = new List<ProductTranslation>
                    {
                        new ProductTranslation
                            {Language = languages.Single(x=>x.LanguageCode =="tr"),ProductName ="Deri Kadın Saat",ProductDescription="Özel Tasarım Kadın Saat Deri"},
                        new ProductTranslation
                            {Language = languages.Single(x=>x.LanguageCode =="en"),ProductName ="Leather Women Watch",ProductDescription="Special Design Women Watch Leather "}
                                            
                    },
                    ProductImages = new List<ProductImage>
                    {
                        new ProductImage
                        {
                            DisplayOrder=1,
                            ProductImageMimeType ="image/jpeg",
                            ProductImagePath = "/Content/ProductImages/derikadinsaat1.jpg"
                        }
                        //,
                        //new ProductImage
                        //{
                        //    DisplayOrder=2,
                        //    ProductImagePath = "path2"
                        //}
                    },
                    ProductVariants = new List<ProductVariant>
                        {
                            new ProductVariant
                            {
                                Enabled=true,
                                ProductVariantTranslations= new List<ProductVariantTranslation>
                                                                {
                                                                    new ProductVariantTranslation
                                                                    {
                                                                        Language =languages.Single(x=> x.LanguageCode =="tr"),
                                                                        ProductVariantDescription ="",
                                                                        ProductVariantName ="Renk"
                                                                    },
                                                                    new ProductVariantTranslation
                                                                    {
                                                                        Language =languages.Single(x=> x.LanguageCode =="en"),
                                                                        ProductVariantDescription ="",
                                                                        ProductVariantName ="Color",
                                                                    }
                                                                },
                                ProductVariantValues = new List<ProductVariantValue>
                                                                {
                                                                     new ProductVariantValue
                                                                     {
                                                                        Inventory = 4,
                                                                        Enabled = true,
                                                                        ProductVariantValueTranslations = new List<ProductVariantValueTranslation>
                                                                            {
                                                                                new ProductVariantValueTranslation
                                                                                {
                                                                                    Language =languages.Single(x=> x.LanguageCode =="tr"),
                                                                                    ProductVariantValueName ="Siyah"
                                                                                },
                                                                                new ProductVariantValueTranslation
                                                                                {
                                                                                    Language =languages.Single(x=> x.LanguageCode =="en"),
                                                                                    ProductVariantValueName ="Black"
                                                                                }
                                                                            }
                                                                     },
                                                                      new ProductVariantValue
                                                                     {
                                                                        Inventory = 8,
                                                                        Enabled = true,
                                                                        ProductVariantValueTranslations = new List<ProductVariantValueTranslation>
                                                                            {
                                                                                new ProductVariantValueTranslation
                                                                                {
                                                                                    Language =languages.Single(x=> x.LanguageCode =="tr"),
                                                                                    ProductVariantValueName ="Beyaz"
                                                                                },
                                                                                new ProductVariantValueTranslation
                                                                                {
                                                                                    Language =languages.Single(x=> x.LanguageCode =="en"),
                                                                                    ProductVariantValueName ="White"
                                                                                }
                                                                            }
                                                                     },
                                                                      new ProductVariantValue
                                                                     {
                                                                        Inventory = 9,
                                                                        Enabled = true,
                                                                        ProductVariantValueTranslations = new List<ProductVariantValueTranslation>
                                                                            {
                                                                                new ProductVariantValueTranslation
                                                                                {
                                                                                    Language =languages.Single(x=> x.LanguageCode =="tr"),
                                                                                    ProductVariantValueName ="Kırmızı"
                                                                                },
                                                                                new ProductVariantValueTranslation
                                                                                {
                                                                                    Language =languages.Single(x=> x.LanguageCode =="en"),
                                                                                    ProductVariantValueName ="Red"
                                                                                }
                                                                            }
                                                                     },
                                                                      new ProductVariantValue
                                                                     {
                                                                        Inventory = 11,
                                                                        Enabled = true,
                                                                        ProductVariantValueTranslations = new List<ProductVariantValueTranslation>
                                                                            {
                                                                                new ProductVariantValueTranslation
                                                                                {
                                                                                    Language =languages.Single(x=> x.LanguageCode =="tr"),
                                                                                    ProductVariantValueName ="Mavi"
                                                                                },
                                                                                new ProductVariantValueTranslation
                                                                                {
                                                                                    Language =languages.Single(x=> x.LanguageCode =="en"),
                                                                                    ProductVariantValueName ="Blue"
                                                                                }
                                                                            }
                                                                     }
                                                                }
                            }
                        }
                },
                #endregion

            #region 4
                new Product
                {
                    Category = categories.Single(x=> x.CategoryTranslations.Single(y=> y.Language.LanguageCode =="tr").CategoryName =="Cüzdan"),
                    ProductURLText="marilyn-monroe-deri-cuzdan",
                    DateCreated=DateTime.Now,
                    DateModified = DateTime.Now,
                    DiscountedPrice = 40,
                    Enabled = true,
                    Inventory =5,
                    OriginalPrice = 40,
                    RelatedProducts = new List<ProductRelation>{},
                    ProductTranslations = new List<ProductTranslation>
                    {
                        new ProductTranslation
                            {Language = languages.Single(x=>x.LanguageCode =="tr"),ProductName ="Marilyn Monroe Deri Cüzdan",ProductDescription="Marilyn Monroe Deri Cüzdan"},
                        new ProductTranslation
                            {Language = languages.Single(x=>x.LanguageCode =="en"),ProductName ="Marilyn Monroe Leather Wallet",ProductDescription="Marilyn Monroe Leather Wallet"}
                                            
                    },
                    ProductImages = new List<ProductImage>
                    {
                        new ProductImage
                        {
                            DisplayOrder=1,
                            ProductImageMimeType ="image/jpeg",
                            ProductImagePath = "/Content/ProductImages/marilynmonroedericuzdan1.jpg"
                        },
                        new ProductImage
                        {
                            DisplayOrder=2,
                            ProductImageMimeType ="image/jpeg",
                            ProductImagePath = "/Content/ProductImages/marilynmonroedericuzdan2.jpg"
                        },
                        new ProductImage
                        {
                            DisplayOrder=2,
                            ProductImageMimeType ="image/jpeg",
                            ProductImagePath = "/Content/ProductImages/marilynmonroedericuzdan3.jpg"
                        }
                    },
                    ProductVariants = new List<ProductVariant>
                        {
                            new ProductVariant
                            {
                                Enabled=true,
                                ProductVariantTranslations= new List<ProductVariantTranslation>
                                                                {
                                                                    new ProductVariantTranslation
                                                                    {
                                                                        Language =languages.Single(x=> x.LanguageCode =="tr"),
                                                                        ProductVariantDescription ="",
                                                                        ProductVariantName ="Renk"
                                                                    },
                                                                    new ProductVariantTranslation
                                                                    {
                                                                        Language =languages.Single(x=> x.LanguageCode =="en"),
                                                                        ProductVariantDescription ="",
                                                                        ProductVariantName ="Color",
                                                                    }
                                                                },
                                ProductVariantValues = new List<ProductVariantValue>
                                                                {
                                                                     new ProductVariantValue
                                                                     {
                                                                        Inventory = 4,
                                                                        Enabled = true,
                                                                        ProductVariantValueTranslations = new List<ProductVariantValueTranslation>
                                                                            {
                                                                                new ProductVariantValueTranslation
                                                                                {
                                                                                    Language =languages.Single(x=> x.LanguageCode =="tr"),
                                                                                    ProductVariantValueName ="Siyah"
                                                                                },
                                                                                new ProductVariantValueTranslation
                                                                                {
                                                                                    Language =languages.Single(x=> x.LanguageCode =="en"),
                                                                                    ProductVariantValueName ="Black"
                                                                                }
                                                                            }
                                                                     },
                                                                      new ProductVariantValue
                                                                     {
                                                                        Inventory = 6,
                                                                        Enabled = true,
                                                                        ProductVariantValueTranslations = new List<ProductVariantValueTranslation>
                                                                            {
                                                                                new ProductVariantValueTranslation
                                                                                {
                                                                                    Language =languages.Single(x=> x.LanguageCode =="tr"),
                                                                                    ProductVariantValueName ="Beyaz"
                                                                                },
                                                                                new ProductVariantValueTranslation
                                                                                {
                                                                                    Language =languages.Single(x=> x.LanguageCode =="en"),
                                                                                    ProductVariantValueName ="White"
                                                                                }
                                                                            }
                                                                     },
                                                                      new ProductVariantValue
                                                                     {
                                                                        Inventory = 15,
                                                                        Enabled = true,
                                                                        ProductVariantValueTranslations = new List<ProductVariantValueTranslation>
                                                                            {
                                                                                new ProductVariantValueTranslation
                                                                                {
                                                                                    Language =languages.Single(x=> x.LanguageCode =="tr"),
                                                                                    ProductVariantValueName ="Kırmızı"
                                                                                },
                                                                                new ProductVariantValueTranslation
                                                                                {
                                                                                    Language =languages.Single(x=> x.LanguageCode =="en"),
                                                                                    ProductVariantValueName ="Red"
                                                                                }
                                                                            }
                                                                     }
                                                                }
                            }
                        }
                }

                #endregion

            #region 5
                ,
                new Product
                {
                    Category = categories.Single(x=> x.CategoryTranslations.Single(y=> y.Language.LanguageCode =="tr").CategoryName =="Kadın Ceket"),
                    ProductURLText="heidi-kaban",
                    DateCreated=DateTime.Now,
                    DateModified = DateTime.Now,
                    DiscountedPrice = 40,
                    Enabled = true,
                    Inventory =5,
                    OriginalPrice = 40,
                    RelatedProducts = new List<ProductRelation>{},
                    ProductTranslations = new List<ProductTranslation>
                    {
                        new ProductTranslation
                            {Language = languages.Single(x=>x.LanguageCode =="tr"),ProductName ="Heidi Kaban",ProductDescription="Heidi Kaban"},
                        new ProductTranslation
                            {Language = languages.Single(x=>x.LanguageCode =="en"),ProductName ="Heidi Coat",ProductDescription="Heidi Coat"}
                                            
                    },
                    ProductImages = new List<ProductImage>
                    {
                        new ProductImage
                        {
                            DisplayOrder=1,
                            ProductImageMimeType ="image/jpeg",
                            ProductImagePath = "/Content/ProductImages/hedicoat1.jpg"
                        }
                    },
                    ProductVariants = new List<ProductVariant>
                        {
                            new ProductVariant
                            {
                                Enabled=true,
                                ProductVariantTranslations= new List<ProductVariantTranslation>
                                                                {
                                                                    new ProductVariantTranslation
                                                                    {
                                                                        Language =languages.Single(x=> x.LanguageCode =="tr"),
                                                                        ProductVariantDescription ="",
                                                                        ProductVariantName ="Renk"
                                                                    },
                                                                    new ProductVariantTranslation
                                                                    {
                                                                        Language =languages.Single(x=> x.LanguageCode =="en"),
                                                                        ProductVariantDescription ="",
                                                                        ProductVariantName ="Color",
                                                                    }
                                                                },
                                ProductVariantValues = new List<ProductVariantValue>
                                                                {
                                                                     new ProductVariantValue
                                                                     {
                                                                        Inventory = 6,
                                                                        Enabled = true,
                                                                        ProductVariantValueTranslations = new List<ProductVariantValueTranslation>
                                                                            {
                                                                                new ProductVariantValueTranslation
                                                                                {
                                                                                    Language =languages.Single(x=> x.LanguageCode =="tr"),
                                                                                    ProductVariantValueName ="Siyah"
                                                                                },
                                                                                new ProductVariantValueTranslation
                                                                                {
                                                                                    Language =languages.Single(x=> x.LanguageCode =="en"),
                                                                                    ProductVariantValueName ="Black"
                                                                                }
                                                                            }
                                                                     },
                                                                      new ProductVariantValue
                                                                     {
                                                                        Inventory = 4,
                                                                        Enabled = true,
                                                                        ProductVariantValueTranslations = new List<ProductVariantValueTranslation>
                                                                            {
                                                                                new ProductVariantValueTranslation
                                                                                {
                                                                                    Language =languages.Single(x=> x.LanguageCode =="tr"),
                                                                                    ProductVariantValueName ="Beyaz"
                                                                                },
                                                                                new ProductVariantValueTranslation
                                                                                {
                                                                                    Language =languages.Single(x=> x.LanguageCode =="en"),
                                                                                    ProductVariantValueName ="White"
                                                                                }
                                                                            }
                                                                     },
                                                                      new ProductVariantValue
                                                                     {
                                                                        Inventory = 12,
                                                                        Enabled = true,
                                                                        ProductVariantValueTranslations = new List<ProductVariantValueTranslation>
                                                                            {
                                                                                new ProductVariantValueTranslation
                                                                                {
                                                                                    Language =languages.Single(x=> x.LanguageCode =="tr"),
                                                                                    ProductVariantValueName ="Kırmızı"
                                                                                },
                                                                                new ProductVariantValueTranslation
                                                                                {
                                                                                    Language =languages.Single(x=> x.LanguageCode =="en"),
                                                                                    ProductVariantValueName ="Red"
                                                                                }
                                                                            }
                                                                     }
                                                                }
                            }
                        }
                }
            #endregion

            #region 6
            ,
                new Product
                {
                    Category = categories.Single(x=> x.CategoryTranslations.Single(y=> y.Language.LanguageCode =="tr").CategoryName == "Yazlık Kadın Ceket"),
                    ProductURLText="modagram-fleo-cicekli-bomber-ceket",
                    DateCreated=DateTime.Now,
                    DateModified = DateTime.Now,
                    DiscountedPrice = 40,
                    Enabled = true,
                    Inventory =5,
                    OriginalPrice = 40,
                    RelatedProducts = new List<ProductRelation>{},
                    ProductTranslations = new List<ProductTranslation>
                    {
                        new ProductTranslation
                            {Language = languages.Single(x=>x.LanguageCode =="tr"),ProductName ="Modagram Fleo Çiçekli Bomber Ceket",ProductDescription="Modagram Fleo Çiçekli Bomber Ceket"},
                        new ProductTranslation
                            {Language = languages.Single(x=>x.LanguageCode =="en"),ProductName ="Modagram Fleo Bomber Coat",ProductDescription="Modagram Fleo Bomber Coat"}
                                            
                    },
                    ProductImages = new List<ProductImage>
                    {
                        new ProductImage
                        {
                            DisplayOrder=1,
                            ProductImageMimeType ="image/jpeg",
                            ProductImagePath = "/Content/ProductImages/modagram-fleo-cicekli-bomber-ceket.jpg"
                        }
                    }
                }

            #endregion

            };
            #endregion

            
            foreach (var item in cities)
            {
                context.Cities.Add(item);
            }

            foreach (var item in products)
            {
                context.Products.Add(item);
            }

            //base.Seed(context);
            var product1 = products.FirstOrDefault(x=>x.ProductURLText=="deri-erkek-saat");
            var product2 = products.FirstOrDefault(x => x.ProductURLText == "deri-kadin-saat");
            var product3 = products.FirstOrDefault(x => x.ProductURLText == "marilyn-monroe-deri-cuzdan");


            //var product1 = context.Products.FirstOrDefault(x => x.ProductTranslations.FirstOrDefault(y => y.Language.LanguageCode == "tr").ProductName == "Deri Erkek Saat");
            //var product2 = context.Products.FirstOrDefault(x => x.ProductTranslations.FirstOrDefault(y => y.Language.LanguageCode == "tr").ProductName == "Deri Kadın Saat");
            //var product3 = context.Products.FirstOrDefault(x => x.ProductTranslations.FirstOrDefault(y => y.Language.LanguageCode == "tr").ProductName == "Marilyn Monroe Deri Cüzdan");
            if (product1 != null)
            { 
            product1.RelatedProducts.Add(new ProductRelation { Product = product1, RelatedProduct = product2, DisplayOrder = 1 });
            }
            if (product2 != null)
            {
                product2.RelatedProducts.Add(new ProductRelation { Product = product2, RelatedProduct = product1, DisplayOrder = 1 });
                product2.RelatedProducts.Add(new ProductRelation { Product = product2, RelatedProduct = product3, DisplayOrder = 2 });
            }
            if (product3 != null)
            {
                product3.RelatedProducts.Add(new ProductRelation { Product = product3, RelatedProduct = product1, DisplayOrder = 1 });
            }
            base.Seed(context);


        }
    }
}