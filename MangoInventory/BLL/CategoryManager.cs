using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MangoInventory.DAL;
using MangoInventory.Models;

namespace MangoInventory.BLL
{
    public class CategoryManager
    {
        CategoryGateway categoryGateway=new CategoryGateway();
        public string CreateCategory(Category category)
        {
            if (GetCategories().Find(a=>a.Name==category.Name)==null)
            {
                if (categoryGateway.CreateCategory(category)>0)
                {
                    return category.Name + " has been created";
                }
                return "Failed";
            }
            else
            {
                return category.Name + " is already exists";
            }
            //throw new NotImplementedException();
        }

        public List<Category> GetCategories()
        {
            return categoryGateway.GetCategories();
        } 

        public string EditCategory(Category category)
        {
            if (categoryGateway.EditCategory(category)>0)
            {
                return "Edited";
            }
            return "Failed";
        }

        public string CreateSubcategory(SubCategory subCategory)
        {
            if (GetSubCategories().Find(a=>a.Name==subCategory.Name)!=null)
            {
                return subCategory.Name + " is already exists";
            }
            else
            {
                if (categoryGateway.CreateSubCategory(subCategory)>0)
                {
                    return subCategory.Name + " has been created successfully";
                }
                return "Failed";
            }
        }

        public List<SubCategory> GetSubCategories()
        {
            return categoryGateway.GetSubCategories();
        }

        public string EditSubCategory(SubCategory subCategory)
        {

            if (categoryGateway.EditSubCategory(subCategory)>0)
            {
                return "Edited";
            }
            return "Failed";
        }

        public string CreateType(DeviceType type)
        {
            if (categoryGateway.CreateType(type)>0)
            {
                return "Created";
            }
            return "Failed";
        }
        public string EditType(DeviceType type)
        {
            if (categoryGateway.CreateType(type) > 0)
            {
                return "Edited";
            }
            return "Failed";
        }
    }
}