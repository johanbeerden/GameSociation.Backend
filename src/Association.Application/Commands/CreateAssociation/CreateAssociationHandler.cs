﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Association.Application.Views;
using Association.Domain.Repositories;
using Common.Application.Commands;
using Common.Application.Queries;

namespace Association.Application.Commands.CreateAssociation
{
    public class CreateAssociationHandler : ICommandHandler<CreateAssociation>
    {
        private readonly IQueryProcessor _queryProcessor;
        private readonly IAssociateRepository _associateRepository;
        private readonly IAssociationRepository _associationRepository;

        public CreateAssociationHandler(IQueryProcessor queryProcessor, IAssociateRepository associateRepository, IAssociationRepository associationRepository)
        {
            _queryProcessor = queryProcessor;
            _associateRepository = associateRepository;
            _associationRepository = associationRepository;
        }

        public async Task Handle(CreateAssociation notification, CancellationToken cancellationToken)
        {
            var associateNameInUse = _queryProcessor.Query<AssociationView>().Any(x => x.Name == notification.Name);
            if(associateNameInUse)
                throw new InvalidOperationException("Association name already in use");

            var associate = await _associateRepository.GetById(notification.AssociateId).ConfigureAwait(false);
            var association = associate.CreateAssociation(notification.Name);
            await _associationRepository.Save(association).ConfigureAwait(false);
        }
    }
}