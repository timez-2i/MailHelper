using Newtonsoft.Json;


namespace MailHelperNet5.Util
{
    public static class CJsonUtil
    {
        public static string ToJSON(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }


    }
}
