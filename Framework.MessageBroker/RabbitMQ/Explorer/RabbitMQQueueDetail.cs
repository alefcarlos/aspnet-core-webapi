namespace Framework.MessageBroker.RabbitMQ.Explorer
{
    public class RabbitMQQueueDetail
    {
        public Arguments arguments { get; set; }
        public bool auto_delete { get; set; }
        public bool durable { get; set; }
        public bool exclusive { get; set; }
        public string idle_since { get; set; }
        public string name { get; set; }
        public string node { get; set; }
        public string state { get; set; }
        public string vhost { get; set; }
    }

    public class Arguments
    {
    }

    public class Backing_Queue_Status
    {
        public float avg_ack_egress_rate { get; set; }
        public float avg_ack_ingress_rate { get; set; }
        public float avg_egress_rate { get; set; }
        public float avg_ingress_rate { get; set; }
        public object[] delta { get; set; }
        public int len { get; set; }
        public string mode { get; set; }
        public int next_seq_id { get; set; }
        public int q1 { get; set; }
        public int q2 { get; set; }
        public int q3 { get; set; }
        public int q4 { get; set; }
        public string target_ram_count { get; set; }
    }

    public class Garbage_Collection
    {
        public int fullsweep_after { get; set; }
        public int max_heap_size { get; set; }
        public int min_bin_vheap_size { get; set; }
        public int min_heap_size { get; set; }
        public int minor_gcs { get; set; }
    }

    public class Message_Stats
    {
        public int ack { get; set; }
        public Ack_Details ack_details { get; set; }
        public int deliver { get; set; }
        public Deliver_Details deliver_details { get; set; }
        public int deliver_get { get; set; }
        public Deliver_Get_Details deliver_get_details { get; set; }
        public int deliver_no_ack { get; set; }
        public Deliver_No_Ack_Details deliver_no_ack_details { get; set; }
        public int get { get; set; }
        public Get_Details get_details { get; set; }
        public int get_no_ack { get; set; }
        public Get_No_Ack_Details get_no_ack_details { get; set; }
        public int publish { get; set; }
        public Publish_Details publish_details { get; set; }
        public int redeliver { get; set; }
        public Redeliver_Details redeliver_details { get; set; }
    }

    public class Ack_Details
    {
        public int rate { get; set; }
    }

    public class Deliver_Details
    {
        public int rate { get; set; }
    }

    public class Deliver_Get_Details
    {
        public int rate { get; set; }
    }

    public class Deliver_No_Ack_Details
    {
        public int rate { get; set; }
    }

    public class Get_Details
    {
        public int rate { get; set; }
    }

    public class Get_No_Ack_Details
    {
        public int rate { get; set; }
    }

    public class Publish_Details
    {
        public int rate { get; set; }
    }

    public class Redeliver_Details
    {
        public int rate { get; set; }
    }

    public class Messages_Details
    {
        public int rate { get; set; }
    }

    public class Messages_Ready_Details
    {
        public int rate { get; set; }
    }

    public class Messages_Unacknowledged_Details
    {
        public int rate { get; set; }
    }

    public class Reductions_Details
    {
        public int rate { get; set; }
    }

}
