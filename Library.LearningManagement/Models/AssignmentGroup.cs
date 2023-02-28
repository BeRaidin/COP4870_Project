namespace Library.LearningManagement.Models
{
    public class AssignmentGroup
    {
        public string Name { get; set; }
        public int Weight { get; set; }

        public AssignmentGroup() 
        {
            Name = string.Empty;
            Weight = 0;
        }

        public AssignmentGroup(string name, int weight)
        {
            Name = name;
            Weight = weight;
        }

        public override string ToString()
        {
            return $"[{Name}] - {Weight}%";
        }

        public AssignmentGroup MakeAssignmentGroup(string? name = null)
        {
            if (name == null)
            {
                Console.WriteLine("What is the name of the assignment group?");
                name = Console.ReadLine() ?? string.Empty;
            }
            
            Console.WriteLine("What is the weight of the assignment group?");
            var groupWeight = Console.ReadLine() ?? string.Empty;
            while (!int.TryParse(groupWeight, out int weightInt) || int.Parse(groupWeight) > 100)
            {
                Console.WriteLine("Please enter a whole number less than 100:");
                groupWeight = Console.ReadLine() ?? "10";
            }

            var assignmentGroup = new AssignmentGroup(name, int.Parse(groupWeight));
            return assignmentGroup;
        }
    }
}
