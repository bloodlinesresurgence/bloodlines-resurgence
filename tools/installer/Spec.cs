using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BloodlinesResurgenceInstaller
{
    /// <summary>
    /// Allows spec tests to be created and run.
    /// </summary>
    public abstract class Spec
    {
        /// <summary>
        /// Callback to notify a spec is done
        /// </summary>
        /// <param name="result"></param>
        public delegate void SpecDone(object result);

        /// <summary>
        /// 
        /// </summary>
        /// <returns>A user-defined result</returns>
        protected delegate void SpecCallback(SpecDone done);

        /// <summary>
        /// A callback for a Timeout
        /// </summary>
        protected delegate void Timeout();

        protected class SpecCallbackEntry {
            public readonly SpecCallback callback;
            public readonly string name;
            public object result;

            public SpecCallbackEntry (string name, SpecCallback c) {
                this.name = name;
                this.callback = c;
            }

            public void run(SpecDone onDone)
            {
                this.callback(delegate(object result)
                {
                    this.result = result;
                    onDone(this);
                });
            }
        }
        protected HashList<SpecCallbackEntry> specs = new HashList<SpecCallbackEntry>();

        protected virtual void _Spec()
        {
            throw new NotImplementedException();
        }

        public void RunSpec(SpecDone onDone)
        {
#if DEBUG
            this._Spec();
            this.run(onDone);
#endif
        }

        private void run(SpecDone onDone)
        {
            // Copy the list of specs
            List<SpecCallbackEntry> list = this.specs.ToList();
            
            SpecDone processor = null;  // Assign to avoid compiler error
            processor = delegate(object result)
            {
                SpecCallbackEntry entry = result as SpecCallbackEntry;
                System.Collections.Hashtable results = new System.Collections.Hashtable();
                results.Add(entry.name, entry.result);

                onDone(results);

                if (0 == list.Count)
                {
                    return;
                }

                entry = list[0];
                list.RemoveAt(0);
                // Call ourselves again!
                entry.run(processor);
            };

            // Run the first to get the ball rolling
            if (list.Count == 0)
            {
                return;
            }
            else
            {
                SpecCallbackEntry entry = list[0];
                list.RemoveAt(0);
                entry.run(processor);
            }
        }

        protected Spec _addSpec(string name, SpecCallback callback)
        {
            if (true == this.specs.ContainsKey(name)) {
                throw new DuplicateSpecException(name);
            }

            this.specs.Add(name, new SpecCallbackEntry(name, callback));

            return this;
        }
    }

    public class DuplicateSpecException : Exception
    {
        public DuplicateSpecException(string name) :
            base("Duplicate spec definition: " + name)
        {
        }
    }
}
