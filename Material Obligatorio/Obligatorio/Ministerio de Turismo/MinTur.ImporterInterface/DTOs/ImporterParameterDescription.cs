using System;

namespace MinTur.ImporterInterface.DTOs
{
    public enum PossibleParameters { File, Text, Number, Flag }

    public class ImporterParameterDescription
    {
        public string Name { get; set; }
        public PossibleParameters Type { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
                return false;

            var parameterDescription = obj as ImporterParameterDescription;
            return Name == parameterDescription.Name && Type == parameterDescription.Type;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Type);
        }
    }
}
