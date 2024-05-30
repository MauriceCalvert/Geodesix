using Flee.CalcEngine.InternalTypes;
using Flee.InternalTypes;
using Flee.PublicTypes;
using System;
using System.Collections.Generic;

namespace Flee.CalcEngine.PublicTypes
{
    public sealed class BatchLoader
    {

        private readonly IDictionary<string, BatchLoadInfo> _myNameInfoMap;

        private readonly DependencyManager<string> _myDependencies;
        internal BatchLoader()
        {
            _myNameInfoMap = new Dictionary<string, BatchLoadInfo>(StringComparer.OrdinalIgnoreCase);
            _myDependencies = new DependencyManager<string>(StringComparer.OrdinalIgnoreCase);
        }

        public void Add(string atomName, string expression, ExpressionContext context)
        {
#pragma warning disable CS0618 // Type or member is obsolete
            Utility.AssertNotNull(atomName, "atomName");
#pragma warning restore CS0618 // Type or member is obsolete
#pragma warning disable CS0618 // Type or member is obsolete
            Utility.AssertNotNull(expression, "expression");
#pragma warning restore CS0618 // Type or member is obsolete
#pragma warning disable CS0618 // Type or member is obsolete
            Utility.AssertNotNull(context, "context");
#pragma warning restore CS0618 // Type or member is obsolete

            BatchLoadInfo info = new BatchLoadInfo(atomName, expression, context);
            _myNameInfoMap.Add(atomName, info);
            _myDependencies.AddTail(atomName);

            ICollection<string> references = this.GetReferences(expression, context);

            foreach (string reference in references)
            {
                _myDependencies.AddTail(reference);
                _myDependencies.AddDepedency(reference, atomName);
            }
        }

        public bool Contains(string atomName)
        {
            return _myNameInfoMap.ContainsKey(atomName);
        }

        internal BatchLoadInfo[] GetBachInfos()
        {
            string[] tails = _myDependencies.GetTails();
            Queue<string> sources = _myDependencies.GetSources(tails);

            IList<string> result = _myDependencies.TopologicalSort(sources);

            BatchLoadInfo[] infos = new BatchLoadInfo[result.Count];

            for (int i = 0; i <= result.Count - 1; i++)
            {
                infos[i] = _myNameInfoMap[result[i]];
            }

            return infos;
        }

        private ICollection<string> GetReferences(string expression, ExpressionContext context)
        {
            IdentifierAnalyzer analyzer = context.ParseIdentifiers(expression);

            return analyzer.GetIdentifiers(context);
        }
    }

}

