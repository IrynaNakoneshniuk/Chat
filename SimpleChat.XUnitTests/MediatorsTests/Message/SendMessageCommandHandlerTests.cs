using Ardalis.Specification;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;
using Moq;
using SimpleChat.BLL.DTO.Message;
using SimpleChat.BLL.Mediator.Messages.Command;
using SimpleChat.DAL.Models.Chats;
using SimpleChat.DAL.Models.Messages;
using SimpleChat.DAL.Models.Users;
using SimpleChat.DAL.Repositories.Interfaces;
using SimpleChat.DAL.Repositories.Interfaces.Base;
using SimpleChat.DAL.Repositories.Interfaces.Chats;
using SimpleChat.DAL.Repositories.Interfaces.Messages;

namespace SimpleChat.XIntegrationTests.MediatorsTests.Messages;

public class SendMessageCommandHandlerTests
{
    private readonly Mock<IChatRepository> _mockChatRepository;
    private readonly Mock<IMessageRepository> _mockMessageRepository;
    private readonly Mock<IUserRepository> _mockUserRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<ILogger<SendMessageCommandHandler>> _mockLogger;
    private readonly SendMessageCommandHandler _handler;

    public SendMessageCommandHandlerTests()
    {
        _mockChatRepository = new Mock<IChatRepository>();
        _mockMessageRepository = new Mock<IMessageRepository>();
        _mockUserRepository = new Mock<IUserRepository>();
        _mockMapper = new Mock<IMapper>();
        _mockLogger = new Mock<ILogger<SendMessageCommandHandler>>();

        _handler = new SendMessageCommandHandler(
            _mockUserRepository.Object,
            _mockChatRepository.Object,
            _mockMessageRepository.Object,
            _mockMapper.Object,
            _mockLogger.Object
        );
    }

    [Fact]
    public async Task Handle_ShouldReturnFailResult_WhenChatNotFound()
    {
        // Arrange
        var command = CreateCommand();
        SetupMockChatRepository(null);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsFailed.Should().BeTrue();
        result.Errors[0].Message.Should().Be("Chat not found for chatId: 1");
    }

    [Fact]
    public async Task Handle_ShouldReturnFailResult_WhenUserNotFound()
    {
        // Arrange
        var command = CreateCommand();
        var chat = CreateChat();
        SetupMockChatRepository(chat);
        SetupMockUserRepository(null);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsFailed.Should().BeTrue();
        result.Errors[0].Message.Should().Be("User not found for userId: 1");
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccessResult_WhenMessageIsCreated()
    {
        // Arrange
        var command = CreateCommand();
        var chat = CreateChat();
        var user = CreateUser();
        var message = CreateMessage();

        SetupMockChatRepository(chat);
        SetupMockUserRepository(user);
        SetupMockMessageRepository(message);
        SetupMockMapper(command.Message, message);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(command.Message);
    }

    private void SetupMockChatRepository(Chat chat)
    {
        _mockChatRepository.Setup(repo => repo.GetFirstOrDefaulAsync(It.IsAny<ISpecification<Chat>>()))
                           .ReturnsAsync(chat);
    }

    private void SetupMockUserRepository(User user)
    {
        _mockUserRepository.Setup(repo => repo.GetFirstOrDefaulAsync(It.IsAny<ISpecification<User>>()))
                           .ReturnsAsync(user);
    }

    private void SetupMockMessageRepository(Message message)
    {
        _mockMessageRepository.Setup(repo => repo.CreateAsync(It.IsAny<Message>()))
                              .ReturnsAsync(message);
        _mockMessageRepository.Setup(repo => repo.SaveChangesAsync()).ReturnsAsync(1);
    }

    private void SetupMockMapper(MessageDto messageDto, Message message)
    {
        _mockMapper.Setup(m => m.Map<Message>(It.IsAny<MessageDto>())).Returns(message);
        _mockMapper.Setup(m => m.Map<MessageDto>(It.IsAny<Message>())).Returns(messageDto);
    }

    private SendMessageCommand CreateCommand()
    {
        return new SendMessageCommand(new MessageDto { ChatId = 1, UserId = 1, Content = "Hello" });
    }

    private Chat CreateChat()
    {
        return new Chat { Id = 1 };
    }

    private User CreateUser()
    {
        return new User { Id = 1 };
    }

    private Message CreateMessage()
    {
        return new Message { Id = 1, Content = "Hello", Timestamp = DateTime.UtcNow, UserId = 1, ChatId = 1 };
    }

}
