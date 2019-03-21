﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;

namespace Microsoft.Bot.Builder.Dialogs.Rules.Steps
{
    public class WaitForInput : Dialog
    {
        public WaitForInput() : base()
        {
            this.Id = OnComputeId();
        }

        protected override string OnComputeId()
        {
            return $"WaitForInput[]";
        }

        public override async Task<DialogTurnResult> BeginDialogAsync(DialogContext dc, object options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Dialog.EndOfTurn;
        }

        public override async Task<DialogConsultation> ConsultDialogAsync(DialogContext dc, CancellationToken cancellationToken = default(CancellationToken))
        {
            return new DialogConsultation()
            {
                Desire = DialogConsultationDesires.CanProcess,
                Processor = async (ctx) =>
                {
                    var activity = ctx.Context.Activity;

                    if (activity.Type == ActivityTypes.Message)
                    {
                        return await ctx.EndDialogAsync(activity.Text, cancellationToken).ConfigureAwait(false);
                    }
                    else
                    {
                        return Dialog.EndOfTurn;
                    }
                }
            };
        }
    }
}
