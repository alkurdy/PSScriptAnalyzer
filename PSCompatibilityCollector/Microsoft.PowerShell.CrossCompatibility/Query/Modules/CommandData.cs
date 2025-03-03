// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Linq;
using Data = Microsoft.PowerShell.CrossCompatibility.Data;

namespace Microsoft.PowerShell.CrossCompatibility.Query
{
    /// <summary>
    /// A readonly query object for PowerShell command data.
    /// </summary>
    public abstract class CommandData
    {
        protected readonly Data.CommandData _commandData;

        /// <summary>
        /// Create a new command data query object from the data object.
        /// </summary>
        /// <param name="name">The name of the command.</param>
        /// <param name="commandData">The command data object describing the command.</param>
        protected CommandData(string name, Data.CommandData commandData)
        {
            _commandData = commandData;
            Name = name;

            var parameters = new Dictionary<string, ParameterData>(StringComparer.OrdinalIgnoreCase);
            var paramAliases = new Dictionary<string, ParameterData>(StringComparer.OrdinalIgnoreCase);

            if (commandData.Parameters != null)
            {
                foreach (KeyValuePair<string, Microsoft.PowerShell.CrossCompatibility.Data.ParameterData> parameter in commandData.Parameters)
                {
                    parameters.Add(parameter.Key, new ParameterData(parameter.Key, parameter.Value));
                }
            }

            if (commandData.ParameterAliases != null)
            {
                foreach (KeyValuePair<string, string> parameterAlias in commandData.ParameterAliases)
                {
                    paramAliases.Add(parameterAlias.Key, parameters[parameterAlias.Value]);
                }
            }

            foreach (KeyValuePair<string, ParameterData> parameterAlias in paramAliases)
            {
                if (!parameters.ContainsKey(parameterAlias.Key))
                {
                    parameters.Add(parameterAlias.Key, parameterAlias.Value);
                }
            }

            Parameters = parameters;
            ParameterAliases = paramAliases;
        }

        /// <summary>
        /// The output types of the command, if any.
        /// </summary>
        public IReadOnlyList<string> OutputType => _commandData.OutputType;

        /// <summary>
        /// The parameter sets of the command, if any.
        /// </summary>
        public IReadOnlyList<string> ParameterSets => _commandData.ParameterSets;

        /// <summary>
        /// The default parameter set of the command, if any.
        /// </summary>
        public string DefaultParameterSet => _commandData.DefaultParameterSet;

        /// <summary>
        /// Parameter aliases of the command.
        /// </summary>
        public IReadOnlyDictionary<string, ParameterData> ParameterAliases { get; }

        /// <summary>
        /// Parameters of the command, including parameters aliases.
        /// </summary>
        public IReadOnlyDictionary<string, ParameterData> Parameters { get; }

        /// <summary>
        /// The command name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// True if this command is bound as a cmdlet (or advanced function), false otherwise.
        /// </summary>
        public abstract bool IsCmdletBinding { get; }
    }
}
