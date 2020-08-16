using AXTConverter.Interface;
using AXTConverter.Models;
using EFCore.BulkExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AXTConverter.Data
{
    public class AXTDataAccess : IAXTDataAccess
    {
        private readonly DatabaseContext _context;

        public AXTDataAccess(DatabaseContext context)
        {
            _context = context;
        }

        public void BulkInsertAXTData(List<AXTModel> axtModel)
        {
            _context.BulkInsert(axtModel);
        }

    }
}
