using MarketHub.BLL.DTOs.Brand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketHub.BLL.Service.Implementation
{
    public class BrandService: IBrandService
    {
        //DI
        private readonly IBrandRepo _brandRepo;
        public BrandService(IBrandRepo brandRepo)
        {
            _brandRepo = brandRepo;
        }

        public GenResponse Create(CreateBrandDTO brandDTO)
        {
            
            GenResponse response = new GenResponse();
            
            //Bl Validation
            if(_brandRepo.IsExistsByName(brandDTO.Name.ToLower()) == true)
            {
                response = CreateResponse(false, "this Brand Name is already in system", brandDTO);
                return response;
            }

            //mapping
            Brand brand = new Brand();
            brand.Name = brandDTO.Name.ToLower();
            brand.Description = brandDTO.Description;
            //brand.IsDeleted = false;
            brand.CreatedAt = DateTime.Now;

            //saving + more Validation
            
            if(_brandRepo.Create(brand) == false)
            {
                response = CreateResponse(false, "There is an Error in adding this brand to system", brandDTO);
                return response;
            }
            //mapping response
            GetBrandDTO getBrandDTO = new GetBrandDTO()
            {
                Id = brand.Id,
                Name = brand.Name,
                Description = brand.Description,
                NumOfCategories = _brandRepo.NumOfCategoriesWithBrand(brand.Id),
                NumOfProducts = _brandRepo.NumOfProductsInBrand(brand.Id)
            };
            response = CreateResponse(true, "Saved SuccessFully", getBrandDTO);
            return response;
        }

        public GenResponse GetById(int id)
        {
            GenResponse response = new GenResponse();

            try
            {
                //Bl Validation
                Brand brand = _brandRepo.GetById(id);
                if (brand == null)  //brand.IsDeleted == true ||
                {
                    response = CreateResponse(false, "No Brand With This id is Exist", null);
                    return response;
                }

                //mapping 
                GetBrandDTO brandDto = new GetBrandDTO();

                brandDto.Id = brand.Id;
                brandDto.Name = brand.Name;
                brandDto.Description = brand.Description;
                brandDto.NumOfCategories = _brandRepo.NumOfCategoriesWithBrand(id);
                brandDto.NumOfProducts = _brandRepo.NumOfProductsInBrand(id);

                //Create Response
                response = CreateResponse(true, "", brandDto);
                return response;
            }
            catch (Exception ex)
            {
                response = CreateResponse(false, $"Internal Server Error : {ex.Message}", null);
                return response;
            }

        }
        public GenResponse GetAll()
        {
            GenResponse response = new GenResponse();
            try
            {
                //BL Validation
                List<Brand> brands = _brandRepo.GetAll();

                if (brands == null || brands.Count == 0)
                {
                    response = CreateResponse(true, "There is no Brands", null);
                    return response;
                }
                //mapping
                List<GetBrandDTO> brandDTOs = new List<GetBrandDTO>();

                foreach (Brand brand in brands)
                {
                    //if (brand.IsDeleted == false)
                    //{
                    GetBrandDTO brandDTO = new GetBrandDTO();
                    
                    brandDTO.Id = brand.Id;
                    brandDTO.Name = brand.Name;
                    brandDTO.Description = brand.Description;
                    brandDTO.NumOfCategories = _brandRepo.NumOfCategoriesWithBrand(brand.Id);
                    brandDTO.NumOfProducts = _brandRepo.NumOfProductsInBrand(brand.Id);

                    brandDTOs.Add(brandDTO);
                    //}
                }
                //create response
                response = CreateResponse(true, "", brandDTOs);
                return response;

            }
            catch (Exception ex)
            {
                response = CreateResponse(true, $"Internal Server Error : {ex.Message}", null);
                return response;
            }
        }

        public GenResponse Update(UpdateBrandDTO brandDTO)
        {
            GenResponse response = new GenResponse();

            //Bl Vaildation
            
            //Wrong Id 
            if(_brandRepo.IsExistsById(brandDTO.Id) == false)
            {
                response = CreateResponse(false, "Wrong Id", null);
                return response;
            }
            //Name not unique
            if(_brandRepo.IsMoreBrandWithSameName(brandDTO.Id,brandDTO.NewName) == true)
            {
                response = CreateResponse(false, "this Brand Name is already in system", null);
                return response;
            }

            //Mapping
            Brand brand = _brandRepo.GetById(brandDTO.Id);

            brand.Name = brandDTO.NewName;
            brand.Description = brandDTO.NewDescription;
            //brand.IsDeleted = false;

            if(_brandRepo.Update(brand) == false)
            {
                response = CreateResponse(false, "There Is An Error in the Server", null);
                return response;
            }

            response = CreateResponse(true, "Updated Successfully", brandDTO);
            return response;
        }
        public GenResponse DeleteById(int id)
        {
            GenResponse response = new GenResponse();

            try
            {
                Brand brand = _brandRepo.GetById(id);

                if (brand == null)
                {
                    response = CreateResponse(false, "Invalid Id", null);
                    return response;
                }

                //if(brand.IsDeleted == true)
                //{
                //    response.IsSuccess = false;
                //    response.Message = "Brand Not In the System";
                //    return response;
                //}

                //brand.IsDeleted = true;
                if (_brandRepo.Delete(brand) == false)
                {
                    response = CreateResponse(false, "internal server error", null);
                    return response;
                }

                //Create response
                
                response = CreateResponse(true, "Deleted Successfully", null);
                return response;

                //bool isBrandExists = _brandRepo.IsExistsById(id);

                //if(isBrandExists == false)
                //{
                //    response.IsSuccess = false;
                //    response.Message = "Invalid Id";
                //    return response;
                //}

                //if (_brandRepo.IsDeletedById(id) == true)
                //{
                //    response.IsSuccess = false;
                //    response.Message = "Brand Not In the System";
                //    return response;
                //}

                //if(_brandRepo.DeleteById(id) == true)
                //{
                //    response.IsSuccess = true;
                //    response.Message = "Deleted Successfully";
                //    response.DataObject = _brandRepo
                //    return response;
                //}
            }
            catch (Exception ex)
            {
                response = CreateResponse(false, "internal server error", null);
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
