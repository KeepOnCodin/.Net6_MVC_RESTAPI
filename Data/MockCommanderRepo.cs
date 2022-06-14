using System.Collections.Generic;
using Commander.API.Models;

namespace Commander.API.Data
{
    public class MockCommanderRepo : ICommanderRepo //Similair to inherentence execept we are implementing
    {
        public void CreateCommand(Command cmd)
        {
            throw new NotImplementedException();
        }

        public void DeleteCommand(Command cmd)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Command> GetAppComands()
        {
            var commmands = new List<Command>()
            {
                new Command() { Id = 65,  HowTo = "Boil an Egg", Line = "Boil Water", Platform = "Kettle and Pan"},
                new Command() { Id = 56,  HowTo = "Boil an Egg", Line = "Boil Water", Platform = "Kettle and Pan"},
                new Command() { Id = 45,  HowTo = "Boil an Egg", Line = "Boil Water", Platform = "Kettle and Pan"}

            };
            return commmands;
        }

        public Command GetCommandById(int id)
        {
            return new Command() { Id = 0,  HowTo = "Boil an Egg", Line = "Boil Water", Platform = "Kettle and Pan"};
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void UpdateCommand(Command cmd)
        {
            throw new NotImplementedException();
        }
    }
}
