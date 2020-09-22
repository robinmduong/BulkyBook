using System;
using System.Collections.Generic;
using System.Text;

namespace BulkyBook.Utility
{
    public static class SD
    {
        //these names below in quotes came from the proc names in the migration (what we used to get our procs on SQL Server's db)
        public const string Proc_CoverType_Create = "usp_CreateCoverType";
        public const string Proc_CoverType_Get = "usp_GetCoverType";
        public const string Proc_CoverType_GetAll = "usp_GetCoverTypes";
        public const string Proc_CoverType_Update = "usp_UpdateCoverType";
        public const string Proc_CoverType_Delete = "usp_DeleteCoverType";
    }
}
