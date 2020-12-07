using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using TagCloud.Settings;

namespace TagCloud.Commands
{
    public class CloudSettingsCommand : ICommand
    {
        private readonly CloudSettings cloudSettings;

        private readonly Dictionary<string, Action<CloudSettings, string>> setters
            = new Dictionary<string, Action<CloudSettings, string>>
            {
                {
                    nameof(CloudSettings.InnerColor),
                    (settings, color) => settings.InnerColor = ColorTranslator.FromHtml($"#{color}")
                },
                {
                    nameof(CloudSettings.OuterColor),
                    (settings, color) => settings.OuterColor = ColorTranslator.FromHtml($"#{color}")
                },
                {
                    nameof(CloudSettings.InnerColorRadius),
                    (settings, radius) => settings.InnerColorRadius = double.Parse(radius)
                },
                {
                    nameof(CloudSettings.OuterColorRadius),
                    (settings, radius) => settings.OuterColorRadius = double.Parse(radius)
                },
                {
                    nameof(CloudSettings.StartRadius),
                    (settings, radius) => settings.StartRadius = double.Parse(radius)
                },
                {
                    "FontName",
                    (settings, fontName) => settings.Font = new Font(new FontFamily(fontName), settings.Font.Size)
                }
            };

        public CloudSettingsCommand(CloudSettings cloudSettings)
        {
            this.cloudSettings = cloudSettings;
            Usage = $"{CommandId} <property> <value>, or without args";
        }

        public string CommandId { get; } = "cs";
        public string Description { get; } = "It is the cloud settings.\nAllows to set the specify cloud settings";
        public string Usage { get; }

        public ICommandResult Handle(string[] args)
        {
            if (args.Length == 0)
                return AllProperties();
            if (args.Length != 2)
                return CommandResult.WithNoArgs();
            var property = args[0];
            var value = args[1];
            if (!setters.TryGetValue(property, out var setter))
                return new CommandResult(false,
                    "Property doesn't exists.\nYou can set the following properties:\n" +
                    string.Join("\n", setters.Select(x => x.Key)));

            try
            {
                setter(cloudSettings, value);
            }
            catch (Exception e)
            {
                return new CommandResult(false, $"An error occurred: '{e.Message}'");
            }

            return new CommandResult(true, $"{property} now is {value}");
        }

        private ICommandResult AllProperties()
        {
            var type = cloudSettings.GetType();
            var builder = new StringBuilder();
            builder.AppendLine("Cloud settings:");
            foreach (var property in type.GetProperties())
            {
                var value = property.GetValue(cloudSettings);
                builder.Append(property.Name);
                builder.Append($" [{property.PropertyType.Name}]");
                builder.Append(": ");
                builder.AppendLine(value != null ? value.ToString() : "null");
            }

            return new CommandResult(true, builder.ToString());
        }
    }
}
