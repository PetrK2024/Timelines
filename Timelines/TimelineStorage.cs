using System.IO;
using System.Text.Json;

namespace Timelines
{
    internal class TimelineStorage
    {
        public static void Save(string path, TimelineProject project)
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.WriteIndented = true;

            string json = JsonSerializer.Serialize(project, options);

            File.WriteAllText(path, json);
        }

        public static TimelineProject Load(string path)
        {
            string json = File.ReadAllText(path);

            TimelineProject project = JsonSerializer.Deserialize<TimelineProject>(json);

            if (project == null)
            {
                project = new TimelineProject();
            }

            return project;
        }
    }
}
