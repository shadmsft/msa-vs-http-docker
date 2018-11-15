using System;

namespace models
{

    public interface Iproduct
    {
        string Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        float Price { get; set; }
    }
    public class product : Iproduct
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
    }
}