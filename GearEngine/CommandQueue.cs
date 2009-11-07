// Gear - Copyright © 2009 Will Shelley. All Rights Reserved.
// Released under the Microsoft Reference License - see license.txt for details.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GearEngine
{
    /// <summary>
    /// Provides a way for commands to be queued and processed and provides events that notify subscribers when new commands are added to the queue.
    /// </summary>
    public sealed class CommandQueue
    {
        #region Events

        /// <summary>
        /// Notifies subscribers when the command queue becomes empty (when the last command is dequeued).
        /// </summary>
        public event EventHandler Empty;

        /// <summary>
        /// Notifies subscribers when the command queue ceases being empty (when one or more commands are added to an empty queue).
        /// </summary>
        public event EventHandler NonEmpty;

        #endregion

        #region Fields

        private readonly Queue<Command> commands = new Queue<Command>(); // encapsulated queue

        #endregion

        #region Methods - Private

        /// <summary>
        /// Raises the <see cref="CommandQueue.Empty"/> event.
        /// </summary>
        /// <param name="e">Event data for the raised event.</param>
        private void OnEmpty(EventArgs e)
        {
            if (this.Empty != null)
                this.Empty(this, e);
        }

        /// <summary>
        /// Raises the <see cref="CommandQueue.NonEmpty"/> event.
        /// </summary>
        /// <param name="e">Event data for the raised event.</param>
        private void OnNonEmpty(EventArgs e)
        {
            if (this.NonEmpty != null)
                this.NonEmpty(this, e);
        }

        #endregion

        #region Methods - Public

        /// <summary>
        /// Removes and returns the <see cref="Command"/> at the beginning of the current <see cref="CommandQueue"/>.
        /// </summary>
        /// <returns>The <see cref="Command"/> at the beginning of the current <see cref="CommandQueue"/>.</returns>
        public Command Dequeue()
        {
            if (this.IsEmpty)
                throw new InvalidOperationException("Queue is empty");

            Command c = this.commands.Dequeue();

            if (this.IsEmpty)
                this.OnEmpty(null);

            return c;
        }

        /// <summary>
        /// Adds a <see cref="Command"/> to the end of the current <see cref="CommandQueue"/>.
        /// </summary>
        /// <param name="command">The <see cref="Command"/> to enqueue.</param>
        public void Enqueue(Command command)
        {
            if (command == null)
                throw new ArgumentNullException("command");

            this.commands.Enqueue(command);

            if (this.Count == 1) // empty queue + 1 item; count == 1
                this.OnNonEmpty(null);
        }

        /// <summary>
        /// Returns the <see cref="Command"/> at the beginning of the current <see cref="CommandQueue"/> without removing it.
        /// </summary>
        /// <returns>The <see cref="Command"/> at the beginning of the current <see cref="CommandQueue"/>.</returns>
        public Command Peek()
        {
            return this.commands.Peek();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Returns the number of commands contained in the queue.
        /// </summary>
        public int Count
        {
            get
            {
                return this.commands.Count;
            }
        }

        /// <summary>
        /// Indicates if the current queue is empty.
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return (this.Count == 0);
            }
        }

        #endregion
    }
}
