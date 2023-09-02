namespace Workouts.RandomQuestions
{
    public class Compiler
    {
        public List<Project> CompiledProjects;

        public Compiler()
        {
            CompiledProjects = new List<Project>();
        }

        public void Compile(Project project)
        {
            foreach (Project dependency in project.Dependencies)
            {
                if (!CompiledProjects.Any(c => c.Name == dependency.Name))
                {
                    Compile(dependency);
                }
            }
            project.Compile();

            CompiledProjects.Add(project);
        }
    }

    #region Data
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

        public static Project GetProject(string name) => GetProjects().First(p => p.Name == name);
    }

    #endregion
}
