using System.Data;

using Dapper;

namespace AiOption.Infrastructure.DataAccess.Extensions {

    public static class DapperExtentions {

        public static DynamicParameters AddParameters(this DynamicParameters This, string name, object value = null,
            DbType? dbType = null, ParameterDirection? direction = null, int? size = null, byte? precision = null,
            byte? scale = null) {
            This.Add(name, value, dbType, direction, size, precision, scale);

            return This;
        }

    }

}