using AXTConverter.Models;
using System;
using System.Collections.Generic;

namespace AXTConverter.Interface
{
    public interface IAXTDataAccess
    {
        void BulkInsertAXTData(List<AXTModel> axtModel);
    }
}
