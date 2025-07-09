
using Microsoft.EntityFrameworkCore;

namespace DemoMVC;

public class Add
{ 
    public static class IdGenerator
    {
        // Hàm sinh mã mới (ví dụ: PS001, EM001, etc.)
        public static string GenerateId<TEntity>(DbSet<TEntity> dbSet, string prefix, string idPropertyName) where TEntity : class
        {
            var last = dbSet
                .AsEnumerable() // dùng LINQ to Objects để truy cập thuộc tính động
                .Select(e => e.GetType().GetProperty(idPropertyName)?.GetValue(e)?.ToString())
                .Where(id => !string.IsNullOrEmpty(id) && id.StartsWith(prefix))
                .OrderByDescending(id => id)
                .FirstOrDefault();

            if (last != null)
            {
                string numberPart = last.Substring(prefix.Length);
                if (int.TryParse(numberPart, out int number))
                {
                    return prefix + (number + 1).ToString("D3");
                }
            }

            return prefix + "001";
        }
    }

}
