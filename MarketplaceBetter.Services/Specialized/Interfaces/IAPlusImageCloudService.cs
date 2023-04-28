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
	}
}
