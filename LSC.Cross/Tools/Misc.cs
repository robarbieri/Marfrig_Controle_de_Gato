using System;

namespace LSC.Cross.Tools
{
    public static class Misc
    {

        public static bool TryCast<TType>(object obj, out TType result)
        {

            try
            {
                result = (TType)Convert.ChangeType(obj, typeof(TType));
                return true;
            }
            catch
            {

                result = default(TType);
                return false;
            }
        }

    }
}
