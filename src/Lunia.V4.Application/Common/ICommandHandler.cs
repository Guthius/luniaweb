using LanguageExt;
using LanguageExt.Common;
using MediatR;

namespace Lunia.V4.Application.Common;

public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, Option<Error>> where TCommand : ICommand;

public interface ICommandHandler<in TCommand, TResult> : IRequestHandler<TCommand, Either<Error, TResult>> where TCommand : ICommand<TResult>;