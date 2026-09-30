using Azure;
using MarketHub.BLL.DTOs.Category;
using MarketHub.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MarketHub.BLL.Service.Implementation
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepo _categoryRepo;
        public CategoryService(ICategoryRepo categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }
        //===========================================================
        public GenResponse GetById(int id)
        {
            GenResponse response = new GenResponse();

            Category category = _categoryRepo.GetById(id);
            //no cat in system
            if (category == null)// || category.IsDeleted == true)
            {
                response = CreateResponse(false, "No Category With This Id", null);
                return response;
            }

            GetCategoryDTO getCategoryDTO = new GetCategoryDTO()
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description ?? "",
                NumOfProducts = _categoryRepo.NumOfProductsInCategory(category.Id),
                NumOfBrands = _categoryRepo.NumOfBrandsInCategory(category.Id),
            };
            //GenResponse response = new GenResponse();
            //response.DataObject = getCategoryDTO;
            //
            response = CreateResponse(true, "", getCategoryDTO);
            return response;
        }

        //===========================================================

        public GenResponse GetAll()
        {
            GenResponse response = new GenResponse();

            List<Category> categories = _categoryRepo.GetAll();
            if (categories == null)
            {
                response = CreateResponse(true, "There Is No Categories Found", null);
                return response;
            }

            //mapping
            List<GetCategoryDTO> categoriesDTO = new List<GetCategoryDTO>();

            foreach (var category in categories)
            {
                //if (category.IsDeleted == false)
                //{
                GetCategoryDTO categoryDTO = new GetCategoryDTO();

                categoryDTO.Id = category.Id;
                categoryDTO.Name = category.Name;
                categoryDTO.Description = category.Description;
                categoryDTO.NumOfBrands = _categoryRepo.NumOfBrandsInCategory(category.Id);
                categoryDTO.NumOfProducts = _categoryRepo.NumOfProductsInCategory(category.Id);

                categoriesDTO.Add(categoryDTO);
                //}
            }

            response = CreateResponse(true, "", categoriesDTO);
            return response;
        }
        public GenResponse Create(CreateCategoryDTO categoryDTO)
        {
            GenResponse response = new GenResponse();
            try
            {
                if (_categoryRepo.IsExistsByName(categoryDTO.Name.ToLower()) == true)
                {
                    response = CreateResponse(false, "This Category Already Exists", null);
                    return response;
                }

                Category category = new Category()
                {
                    Name = categoryDTO.Name.ToLower(),
                    Description = categoryDTO.Description,
                    CreatedAt = DateTime.Now,
                    //IsDeleted = false,
                };

                if(_categoryRepo.Create(category) == false)
                {
                    response = CreateResponse(false, "Internal Server Error", null);
                    return response;
                }

                //mapping response DTO 
                GetCategoryDTO getCategoryDTO = new GetCategoryDTO()
                {
                    Id = category.Id,
                    Name = categoryDTO.Name,
                    Description = categoryDTO.Description,
                    NumOfBrands = _categoryRepo.NumOfBrandsInCategory(category.Id),
                    NumOfProducts = _categoryRepo.NumOfProductsInCategory(category.Id)
                };

                //Create Response
                response = CreateResponse(true, "Created Successfully", getCategoryDTO);
                return response;
            }
            catch (Exception ex)
            {
                response = CreateResponse(false, "Internal Server Error", null);
                return response;
            }
        }

        public GenResponse Update(UpdateCategoryDTO categoryDTO)
        {
            GenResponse response = new GenResponse();
            try
            {
                //BL Validation
                if (_categoryRepo.IsExistsById(categoryDTO.Id) == false)
                {
                    response = CreateResponse(false, "Category Not In the System", categoryDTO);
                    return response;
                }

                //if: Id != id && Name == name 
                if (_categoryRepo.IsMoreCategoryWithSameName(categoryDTO.Id, categoryDTO.NewName))
                {
                    response = CreateResponse(false, "There Is An Category with this Name, change it", categoryDTO);
                    return response;
                }

                Category category = _categoryRepo.GetById(categoryDTO.Id);

                //if (category.IsDeleted == true)
                //{
                //    category.IsDeleted = false;
                //}

                //Mapping
                category.Name = categoryDTO.NewName;
                category.Description = categoryDTO.NewDescription;

                _categoryRepo.Update(category);

                //Create the Response 
                response = CreateResponse(true, "Updated Successfully", categoryDTO);
                return response;
            }
            catch(Exception ex)
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
                //Bl validation
                if (_categoryRepo.IsExistsById(id) == false)
                {
                    response = CreateResponse(false, "No Category With This Id in the System", null);
                    return response;
                }

                //if (_categoryRepo.IsDeletedById(id) == true)
                //{
                //    response.IsSuccess = false;
                //    response.Message = "This Category is Already Deleted";
                //    return response;
                //}

                if (_categoryRepo.Delete(id) == false)
                {
                    response = CreateResponse(false, "Failed to Delete the Category", null);
                    return response;
                }

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
