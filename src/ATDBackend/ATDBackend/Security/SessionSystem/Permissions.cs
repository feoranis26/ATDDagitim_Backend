namespace ATDBackend.Security.SessionSystem
{
    //SHIFT: 1 << (x - 1) -> x = ENUM VALUE
    public enum Permission : uint
    {
        None = 0,

        PERMISSION_ADMIN = 1,

        ORDER_SELF_READ = 2,
        ORDER_SELF_CREATE = 3,
        ORDER_GLOBAL_READ = 4,
        ORDER_GLOBAL_MODIFY = 5,


        PRODUCT_CREATE = 6,
        PRODUCT_MODIFY = 7,
        PRODUCT_CONTRIBUTOR_MODIFY = 8,

        SCHOOL_SELF_READ = 9,
        SCHOOL_SELF_PURCHASEPRODUCT = 10,
        SCHOOL_GLOBAL_CREATE = 11,
        SCHOOL_GLOBAL_READ = 12,
        SCHOOL_GLOBAL_MODIFY = 13,

        USER_SELF_READ = 14
    }


    public class Permissions
    {
        public static ulong ConvertPermissionsToBitwise(params Permission[] permissions)
        {
            int result = 0;

            foreach (Permission permission in permissions)
            {
                if (permission == Permission.None) continue;

                ulong permVal = (ulong)permission;

                result |= (int)1 << (int)(permVal - 1);
            }
            return (ulong)result;
        }

        public static Dictionary<int, string> GetPermissions()
        {
            Dictionary<int, string> dict = new Dictionary<int, string>();

            List<string> names = Enum.GetNames(typeof(Permission)).ToList();
            List<int> vals = ((int[])Enum.GetValues(typeof(Permission))).ToList();

            for (int i = 0; i < vals.Count; i++)
            {
                dict.Add(vals[i], names[i]);
            }

            return dict;
        }
    }
}
