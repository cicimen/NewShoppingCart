using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

//sil sonra
using System.Data.Entity;

//using ShoppingCart.Domain.Abstract;
using ShoppingCart.Data.Entity;
using ShoppingCart.UI.Models;


using System.Drawing;

namespace ShoppingCart.UI.Controllers
{
    public class ImageController : ShoppingCartControllerBase
    {
        public FileContentResult GetImage(int productImageID)
        {
            ProductImage productImage = ProductImages.GetBy(productImageID);
            if (productImage != null)
            {
                return File( System.IO.File.ReadAllBytes(HttpContext.Server.MapPath(productImage.ProductImagePath)), productImage.ProductImageMimeType);
            }
            else
            {
                return null;
            }
        }
        public FileContentResult GetImageThumbnail(int productImageID)
        {
            ProductImage productImage = ProductImages.GetBy(productImageID);
            if (productImage != null)
            {
                string imagePath =HttpContext.Server.MapPath(productImage.ProductImagePath);
                string thumbnailImagePath = imagePath.Substring(0, imagePath.LastIndexOf(".")) + "-thumbnail" 
                    + imagePath.Substring(imagePath.LastIndexOf("."),imagePath.Length-imagePath.LastIndexOf("."));
                if (System.IO.File.Exists(thumbnailImagePath))
                {
                    return File(System.IO.File.ReadAllBytes(thumbnailImagePath), productImage.ProductImageMimeType);
                }

                Image.GetThumbnailImageAbort myCallback = new Image.GetThumbnailImageAbort(ThumbnailCallback);
                Bitmap myBitmap = new Bitmap(HttpContext.Server.MapPath(productImage.ProductImagePath));
                int height = (int)((double)myBitmap.Height*60 / (double)myBitmap.Width);
                Image myThumbnail = myBitmap.GetThumbnailImage(60,height, myCallback, IntPtr.Zero);
                myThumbnail.Save(thumbnailImagePath);
                return File(System.IO.File.ReadAllBytes(thumbnailImagePath), productImage.ProductImageMimeType);
            }
            else
            {
                return null;
            }
        }

        private bool ThumbnailCallback()
        {
            return false;
        }
	}
}