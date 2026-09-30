
using Azure;
using MarketHub.BLL.DTOs.Products;
using MarketHub.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketHub.BLL.Service.Implementation
{
    public class ProductService :IProductService
    {
        private readonly IProductRepo _productRepo;
        private readonly ICategoryRepo _categoryRepo;
        private readonly IBrandRepo _brandRepo;
        public ProductService(IProductRepo productRepo,
                              ICategoryRepo categoryRepo,
                              IBrandRepo brandRepo)
        {
            _productRepo = productRepo;
            _categoryRepo = categoryRepo;
            _brandRepo = brandRepo;
        }
        public GenResponse GetAll()
        {
            GenResponse response = new GenResponse();
            
            List<Product> prdcts = _productRepo.GetAll();
            if (prdcts == null)
            {
                return null;
            }

            //mapping
            List<ProductDto> prdctsDto = new List<ProductDto>();
            foreach (var prdct in prdcts)
            {
                //if (prdct.IsDeleted == false)
                //{
                ProductDto productDto = new ProductDto();
                
                productDto.Id = prdct.Id;
                productDto.Name = prdct.Name;
                productDto.Description = prdct.Description;
                productDto.Price = prdct.Price;
                productDto.StockQuantity = prdct.StockQuantity;
                productDto.BrandName = prdct.Brand?.Name ?? "";
                productDto.CategoryName = prdct.Category?.Name ?? "";
                productDto.CategoryId = prdct.CategoryId;
                productDto.BrandId = prdct.BrandId;


                //if (prdct.Category?.IsDeleted == true)
                //        productDto.CategoryId = null;

                //    if (prdct.Brand?.IsDeleted == true)
                //        productDto.BrandId = null;

                prdctsDto.Add(productDto);
                //}
            }
            response = CreateResponse(true, "", prdctsDto);
            return response;
        }
        public GenResponse GetById(int id) 
        {
            GenResponse response = new GenResponse();

            Product product = _productRepo.GetById(id);
            if (product == null)// || product.IsDeleted == true)
            {
                response = CreateResponse(false, "Invalid Id", null);
                return response;
            }

            ProductDto productDto = new ProductDto();

            productDto.Id = product.Id;
            productDto.Name = product.Name;
            productDto.Description = product.Description;
            productDto.Price = product.Price;
            productDto.StockQuantity = product.StockQuantity;
            productDto.CategoryName = product.Category?.Name;
            productDto.BrandName = product.Brand?.Name;
            productDto.CategoryId = product.CategoryId;
            productDto.BrandId = product.BrandId;

            //if (product.Category?.IsDeleted == true)
            //    productDto.CategoryId = null;

            //if (product.Brand?.IsDeleted == true)
            //    productDto.BrandId = null;

            response = CreateResponse(true, "", productDto);
            return response;
        }


        public GenResponse Create(CreateProductDto productDto)
        {
            GenResponse response = new GenResponse();

            //BL Validation
            if (_categoryRepo.IsExistsById(productDto.CategoryId) == false)
            {
                response = CreateResponse(false, "Invalid CategoryId", null);
                return response;
            }
            if (_brandRepo.IsExistsById(productDto.BrandId) == false)
            {
                response = CreateResponse(false, "Invalid BrandId", null);
                return response;
            }
            if (_productRepo.IsExistsByName(productDto.Name) == true)
            {
                response = CreateResponse(false, "This Name Already in the System", null);
                return response;
            }

            Product product = new Product() 
            {
                Name = productDto.Name,
                Description = productDto.Description ?? "",
                Price = productDto.Price,
                StockQuantity = productDto.StockQuantity,
                CategoryId = productDto.CategoryId,
                BrandId = productDto.BrandId,
                CreatedAt = DateTime.Now,
                //IsDeleted = false,
            };

            if (_productRepo.Create(product) == false)
            {
                response = CreateResponse(false, "internal server error", null);
                return response;
            }

            //mapping

            Product productWithBrandAndCategory = _productRepo.GetById(product.Id);

            ProductDto getProductDto = new ProductDto();
            getProductDto.Id = productWithBrandAndCategory.Id;
            getProductDto.Name = productWithBrandAndCategory.Name;
            getProductDto.Description = productWithBrandAndCategory.Description ?? "";
            getProductDto.Price = productWithBrandAndCategory.Price;
            getProductDto.StockQuantity = productWithBrandAndCategory.StockQuantity;
            getProductDto.CategoryId = productWithBrandAndCategory.CategoryId;
            getProductDto.BrandId = productWithBrandAndCategory.BrandId;
            getProductDto.BrandName = productWithBrandAndCategory.Brand?.Name ?? "";
            getProductDto.CategoryName = productWithBrandAndCategory.Category?.Name ?? "";
            //create response
            response = CreateResponse(true, "Created Successfully", getProductDto);

            return response;
        }
        public GenResponse Update(UpdateProductDto updtPrdctDto)
        {
            GenResponse response = new GenResponse();
            
            try
            {
                //BL Validation
                if (_productRepo.IsExistsById(updtPrdctDto.Id) == false)
                {
                    response = CreateResponse(false, "Product Id is Wrong ", null);
                    return response;
                }
                if (_categoryRepo.IsExistsById(updtPrdctDto.CategoryId) == false)
                {
                    response = CreateResponse(false, "CategoryId Is Invalid", null);
                    return response;
                }
                if (_brandRepo.IsExistsById(updtPrdctDto.BrandId) == false)
                {
                    response = CreateResponse(false, "BrandId Is Invalid", null);
                    return response;
                }

                //Mapping 
                Product product = _productRepo.GetById(updtPrdctDto.Id);

                product.Name = updtPrdctDto.Name;
                product.Description = updtPrdctDto.Description ?? "";
                product.Price = updtPrdctDto.Price;
                product.StockQuantity = updtPrdctDto.StockQuantity;
                product.CategoryId = updtPrdctDto.CategoryId;
                product.BrandId = updtPrdctDto.BrandId;

                //Updating 
                if (_productRepo.Update(product) == false)
                {
                    response = CreateResponse(false, "Internal Server Error", null);
                    return response;
                }

                //mapping Response Dto
                Product productWithBrandAndCategory = _productRepo.GetById(product.Id);

                ProductDto getProductDto = new ProductDto()
                {
                    Id = productWithBrandAndCategory.Id,
                    Name = productWithBrandAndCategory.Name,
                    Description = productWithBrandAndCategory.Description ?? "",
                    Price = productWithBrandAndCategory.Price,
                    StockQuantity = productWithBrandAndCategory.StockQuantity,
                    CategoryId = productWithBrandAndCategory.CategoryId,
                    BrandId = productWithBrandAndCategory.BrandId,
                    BrandName = productWithBrandAndCategory.Brand?.Name ?? "",
                    CategoryName = productWithBrandAndCategory.Category?.Name ?? ""
                };

                response = CreateResponse(true,"Updated Successfully", getProductDto);

                return response;
            }
            catch (Exception ex)
            {
                response = CreateResponse(false, "Internal Server Error", null);
                return response;
            }

        }
        public GenResponse Delete(int id)
        {
            GenResponse response = new GenResponse();
            try
            {
                Product product = _productRepo.GetById(id);
                if (product == null)// || product.IsDeleted == true)
                {
                    response = CreateResponse(false, "No Product with this Id in the System", null);
                    return response;
                }

                _productRepo.Delete(product);

                response = CreateResponse(true, "Successfully Deleted", null);
                return response;

            }
            catch (Exception ex)
            {
                response = CreateResponse(false, "Internal Server Error", null);
                return response;
            }
        }
        private GenResponse CreateResponse(bool IsSuccess, string Message, dynamic? DataObject)
        {
            GenResponse response = new GenResponse();
            response.IsSuccess = IsSuccess;
            response.Message = Message;
            response.DataObject = DataObject;
            return response;
        }
    }
}
