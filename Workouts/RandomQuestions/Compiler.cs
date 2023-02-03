

namespace Workouts.RandomQuestions
{
    public class Compiler
    {
        public List<Project> CompiledProjects = new List<Project>();

        public List<Project> ProjectsForCompile = Dummy.GetProjects();

        public void Compile()
        {
            foreach (var project in ProjectsForCompile)
            {
                if (!CompiledProjects.Contains(project))
                {
                    Compile(project);
                }

            }
        }
        private void Compile(Project project)
        {
            foreach (Project project1 in project.Dependencies)
            {
                if (!CompiledProjects.Contains(project1))
                {
                    Compile(project1);
                }
            }

            project.Compile();

            CompiledProjects.Add(project);
        }
    }

    public class Project
    {
        public Project(string name, params Project[] projects)
        {
            Name = name;
            Dependencies = new List<Project>();
            foreach (Project project in projects)
            {
                Dependencies.Add(project);
            }
        }

        public string Name { get; set; }
        public List<Project> Dependencies { get; set; }

        public void Compile()
        {
            Console.WriteLine(Name + "Compiled");
        }
    }

    static class Dummy
    {
        public static List<Project> GetProjects()
        {
            Project project = new Project("Project1");
            Project project2 = new Project("Project2", project);
            Project project3 = new Project("Project3", project, project2);
            Project project4 = new Project("Project4", project, project2, project3);

            return new List<Project> { project4, project3, project2, project };
        }
    }
}
