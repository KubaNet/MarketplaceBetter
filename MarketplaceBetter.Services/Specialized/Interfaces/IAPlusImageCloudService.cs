using MarketplaceBetter.Domain.Entities.Amazon.APlusContents;
using MarketplaceBetter.Services.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Services.Specialized.Interfaces
{
	public interface IAPlusImageCloudService
	{
		APlusImageUploadResult Upload(MemoryStream imageStream, string fileName);

        APlusImage CopyImage(string cloudId, string version, string newFileName);

        string GetOriginalUrl(string cloudId, string version);

        string GetBiggerUrlForLists(string cloudId, string version);

        void Delete(IList<string> cloudIds);
    }
}
