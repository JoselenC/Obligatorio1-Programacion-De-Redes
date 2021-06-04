// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Protos/posts.proto
// </auto-generated>
#pragma warning disable 0414, 1591
#region Designer generated code

using grpc = global::Grpc.Core;

namespace ClientGRPC {
  public static partial class PostGrpc
  {
    static readonly string __ServiceName = "posts.PostGrpc";

    static void __Helper_SerializeMessage(global::Google.Protobuf.IMessage message, grpc::SerializationContext context)
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (message is global::Google.Protobuf.IBufferMessage)
      {
        context.SetPayloadLength(message.CalculateSize());
        global::Google.Protobuf.MessageExtensions.WriteTo(message, context.GetBufferWriter());
        context.Complete();
        return;
      }
      #endif
      context.Complete(global::Google.Protobuf.MessageExtensions.ToByteArray(message));
    }

    static class __Helper_MessageCache<T>
    {
      public static readonly bool IsBufferMessage = global::System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(global::Google.Protobuf.IBufferMessage)).IsAssignableFrom(typeof(T));
    }

    static T __Helper_DeserializeMessage<T>(grpc::DeserializationContext context, global::Google.Protobuf.MessageParser<T> parser) where T : global::Google.Protobuf.IMessage<T>
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (__Helper_MessageCache<T>.IsBufferMessage)
      {
        return parser.ParseFrom(context.PayloadAsReadOnlySequence());
      }
      #endif
      return parser.ParseFrom(context.PayloadAsNewBuffer());
    }

    static readonly grpc::Marshaller<global::ClientGRPC.AddPostsRequest> __Marshaller_posts_AddPostsRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::ClientGRPC.AddPostsRequest.Parser));
    static readonly grpc::Marshaller<global::ClientGRPC.AddPostsReply> __Marshaller_posts_AddPostsReply = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::ClientGRPC.AddPostsReply.Parser));
    static readonly grpc::Marshaller<global::ClientGRPC.DeletePostRequest> __Marshaller_posts_DeletePostRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::ClientGRPC.DeletePostRequest.Parser));
    static readonly grpc::Marshaller<global::ClientGRPC.DeletePostReply> __Marshaller_posts_DeletePostReply = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::ClientGRPC.DeletePostReply.Parser));
    static readonly grpc::Marshaller<global::ClientGRPC.ModifyPostRequest> __Marshaller_posts_ModifyPostRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::ClientGRPC.ModifyPostRequest.Parser));
    static readonly grpc::Marshaller<global::ClientGRPC.ModifyPostReply> __Marshaller_posts_ModifyPostReply = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::ClientGRPC.ModifyPostReply.Parser));

    static readonly grpc::Method<global::ClientGRPC.AddPostsRequest, global::ClientGRPC.AddPostsReply> __Method_AddPost = new grpc::Method<global::ClientGRPC.AddPostsRequest, global::ClientGRPC.AddPostsReply>(
        grpc::MethodType.Unary,
        __ServiceName,
        "AddPost",
        __Marshaller_posts_AddPostsRequest,
        __Marshaller_posts_AddPostsReply);

    static readonly grpc::Method<global::ClientGRPC.DeletePostRequest, global::ClientGRPC.DeletePostReply> __Method_DeletePost = new grpc::Method<global::ClientGRPC.DeletePostRequest, global::ClientGRPC.DeletePostReply>(
        grpc::MethodType.Unary,
        __ServiceName,
        "DeletePost",
        __Marshaller_posts_DeletePostRequest,
        __Marshaller_posts_DeletePostReply);

    static readonly grpc::Method<global::ClientGRPC.ModifyPostRequest, global::ClientGRPC.ModifyPostReply> __Method_ModifyPost = new grpc::Method<global::ClientGRPC.ModifyPostRequest, global::ClientGRPC.ModifyPostReply>(
        grpc::MethodType.Unary,
        __ServiceName,
        "ModifyPost",
        __Marshaller_posts_ModifyPostRequest,
        __Marshaller_posts_ModifyPostReply);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::ClientGRPC.PostsReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of PostGrpc</summary>
    [grpc::BindServiceMethod(typeof(PostGrpc), "BindService")]
    public abstract partial class PostGrpcBase
    {
      public virtual global::System.Threading.Tasks.Task<global::ClientGRPC.AddPostsReply> AddPost(global::ClientGRPC.AddPostsRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      public virtual global::System.Threading.Tasks.Task<global::ClientGRPC.DeletePostReply> DeletePost(global::ClientGRPC.DeletePostRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      public virtual global::System.Threading.Tasks.Task<global::ClientGRPC.ModifyPostReply> ModifyPost(global::ClientGRPC.ModifyPostRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Client for PostGrpc</summary>
    public partial class PostGrpcClient : grpc::ClientBase<PostGrpcClient>
    {
      /// <summary>Creates a new client for PostGrpc</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      public PostGrpcClient(grpc::ChannelBase channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for PostGrpc that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      public PostGrpcClient(grpc::CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      protected PostGrpcClient() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      protected PostGrpcClient(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      public virtual global::ClientGRPC.AddPostsReply AddPost(global::ClientGRPC.AddPostsRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return AddPost(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::ClientGRPC.AddPostsReply AddPost(global::ClientGRPC.AddPostsRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_AddPost, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::ClientGRPC.AddPostsReply> AddPostAsync(global::ClientGRPC.AddPostsRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return AddPostAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::ClientGRPC.AddPostsReply> AddPostAsync(global::ClientGRPC.AddPostsRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_AddPost, null, options, request);
      }
      public virtual global::ClientGRPC.DeletePostReply DeletePost(global::ClientGRPC.DeletePostRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return DeletePost(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::ClientGRPC.DeletePostReply DeletePost(global::ClientGRPC.DeletePostRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_DeletePost, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::ClientGRPC.DeletePostReply> DeletePostAsync(global::ClientGRPC.DeletePostRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return DeletePostAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::ClientGRPC.DeletePostReply> DeletePostAsync(global::ClientGRPC.DeletePostRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_DeletePost, null, options, request);
      }
      public virtual global::ClientGRPC.ModifyPostReply ModifyPost(global::ClientGRPC.ModifyPostRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return ModifyPost(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::ClientGRPC.ModifyPostReply ModifyPost(global::ClientGRPC.ModifyPostRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_ModifyPost, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::ClientGRPC.ModifyPostReply> ModifyPostAsync(global::ClientGRPC.ModifyPostRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return ModifyPostAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::ClientGRPC.ModifyPostReply> ModifyPostAsync(global::ClientGRPC.ModifyPostRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_ModifyPost, null, options, request);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      protected override PostGrpcClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new PostGrpcClient(configuration);
      }
    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static grpc::ServerServiceDefinition BindService(PostGrpcBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_AddPost, serviceImpl.AddPost)
          .AddMethod(__Method_DeletePost, serviceImpl.DeletePost)
          .AddMethod(__Method_ModifyPost, serviceImpl.ModifyPost).Build();
    }

    /// <summary>Register service method with a service binder with or without implementation. Useful when customizing the  service binding logic.
    /// Note: this method is part of an experimental API that can change or be removed without any prior notice.</summary>
    /// <param name="serviceBinder">Service methods will be bound by calling <c>AddMethod</c> on this object.</param>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static void BindService(grpc::ServiceBinderBase serviceBinder, PostGrpcBase serviceImpl)
    {
      serviceBinder.AddMethod(__Method_AddPost, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::ClientGRPC.AddPostsRequest, global::ClientGRPC.AddPostsReply>(serviceImpl.AddPost));
      serviceBinder.AddMethod(__Method_DeletePost, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::ClientGRPC.DeletePostRequest, global::ClientGRPC.DeletePostReply>(serviceImpl.DeletePost));
      serviceBinder.AddMethod(__Method_ModifyPost, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::ClientGRPC.ModifyPostRequest, global::ClientGRPC.ModifyPostReply>(serviceImpl.ModifyPost));
    }

  }
}
#endregion
