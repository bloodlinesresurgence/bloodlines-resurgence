using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BloodlinesResurgenceInstaller
{
    /// <summary>
    /// A simple event emitter.
    /// </summary>
    public class EventEmitter : Spec
    {
        public delegate void Event (object[] p);
        private HashList<List<Event>> events = new HashList<List<Event>>();

        /// <summary>
        /// Create a new instance of the event emitter.
        /// </summary>
        public EventEmitter()
        {   
        }

        /// <summary>
        /// Add a listener for the specified event.
        /// </summary>
        /// <param name="ev">Event to listen for.</param>
        /// <param name="callback">Event handler.</param>
        /// <returns>EventEmitter</returns>
        public EventEmitter addListener(string ev, Event callback)
        {
            ev = ev.ToLower();

            if (false == this.events.ContainsKey(ev)) {
                this.events.Add(ev, new List<Event>());
            }

            this.events[ev].Add(callback);

            return this;
        }

        /// <summary>
        /// Remove all listeners ascociated with the specified event.
        /// </summary>
        /// <param name="ev">Event to remove listeners for</param>
        /// <returns>EventEmitter</returns>
        public EventEmitter removeListener(string ev)
        {
            ev = ev.ToLower();

            if (true == this.events.ContainsKey(ev))
            {
                this.events[ev].Clear();
            }

            return this;
        }

        /// <summary>
        /// Emit an event to all listeners for that event.
        /// </summary>
        /// <param name="ev">Event to emit.</param>
        /// <param name="p">Any parameters to pass along to handlers.</param>
        /// <returns></returns>
        protected EventEmitter emit(string ev, params object[] p)
        {
            ev = ev.ToLower();

            if (true == this.events.ContainsKey(ev))
            {
                foreach (Event handler in this.events[ev])
                {
                    this.performEmit(handler, p);
                }
            }

            return this;
        }

        /// <summary>
        /// Perform the actual emit. This is done in a separate thread.
        /// </summary>
        /// <param name="handler">Handler to call.</param>
        /// <param name="p">Parameters to pass in.</param>
        private void performEmit(Event handler, object[] p)
        {
            System.Threading.Thread thread = new System.Threading.Thread(() => handler(p));
            thread.Start();
        }

        protected override void _Spec()
        {
            // Spec tests!
            this._addSpec("addListener", delegate(Spec.SpecDone done)
            {
                EventEmitter self = new EventEmitter();
                self.addListener("test", delegate(object[] p) {});
                done(self.events.Count > 0);
            });

            this._addSpec("emit", delegate(Spec.SpecDone done)
            {
                EventEmitter self = new EventEmitter();
                self.addListener("test", delegate(object[] p)
                {
                    done(true);
                });
                self.emit("test");
            });

            this._addSpec("emit with params", delegate(Spec.SpecDone done)
            {
                EventEmitter self = new EventEmitter();
                self.addListener("test", delegate(object[] p)
                {
                    bool match = (p.Length == 3 && (int)p[0] == 1 && (int)p[1] == 2 && (int)p[2] == 3);
                    done(match);
                });
                self.emit("test", 1, 2, 3);
            });
        }
    }
}
