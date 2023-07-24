using MarketplaceBetter.Domain.Model.Amazon.APlusContents;
using MarketplaceBetter.Domain.Model.Amazon.Inventory;
using MarketplaceBetter.Domain.Model.Base;
using MarketplaceBetter.Domain.Model.Catalog.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Domain.Base.Interfaces
{
    public interface IEntityStatusService
    {
        IList<EntityStatusModel> GetAll();

        void ChangeStatus(IList<ProductModel> products, EntityStatusModel status);

        void ChangeStatus(IList<VariantModel> variants, EntityStatusModel status);

        void ChangeStatus(IList<ParentModel> parents, EntityStatusModel status);

        void ChangeStatus(IList<ChildModel> childs, EntityStatusModel status);

		void ChangeStatus(IList<APlusContentModel> contents, EntityStatusModel status);
	}
}
