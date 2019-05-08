namespace Chapter03._3_11
{
    class NullUser : IUser
    {
        public void IncrementsSessionTicket()
        {
            // 아무 일도 하지 않는다.
        }

        public bool IsNull
        {
            get
            {
                return true;
            }
        }
    }
}