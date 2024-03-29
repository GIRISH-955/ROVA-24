using System;

namespace ROVA_24.Model
{
    public class Notification
    {
        public int NotificationId {  get; set; }
        public int CustomerId { get; set; } // Foreign key referencing Customer
        public String Message {  get; set; }
        public bool IsRead {  get; set; }
        public String Timestamp {  get; set; }

    }
}
