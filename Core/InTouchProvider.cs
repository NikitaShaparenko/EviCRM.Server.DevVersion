using EviCRM.Server.Core.EntityFramework;

namespace EviCRM.Server.Core
{
    public class InTouchProvider
    {
        List<intouch_elem> inTouch;

        public InTouchProvider()
        {
            inTouch = new List<intouch_elem>();
        }

        public void UpdateStatus(schema_users user_)
        {
            bool replaced = false;
            if (inTouch.Count > 0)
            {
                for (int i = 0; i < inTouch.Count; i++)
                {
                    if (inTouch[i].user == user_)
                    {
                        inTouch[i].last_access_time_token = DateTime.Now;
                        replaced = true;
                    }
                }
            }
            if (replaced == false)
            {
                intouch_elem int_e = new(user_);
                inTouch.Add(int_e);
            }

        }

        public DateTime? GetTimeTokenByUser(schema_users user_)
        {
            if (inTouch.Count > 0)
            {
                for (int i = 0; i < inTouch.Count; i++)
                {
                    if (inTouch[i].user == user_)
                    {
                       return inTouch[i].last_access_time_token;
                    }
                }
            }
            return null;
        }
    }

    public class intouch_elem
    {
        public DateTime last_access_time_token { get; set; }

        public schema_users user { get; set; }

        public intouch_elem(schema_users user_)
        {
            last_access_time_token = DateTime.Now;
            user = user_;
        }

    }
}
