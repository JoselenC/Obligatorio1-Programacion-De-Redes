// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Protos/posts.proto
// </auto-generated>
#pragma warning disable 0414, 1591
#region Designer generated code

using grpc = global::Grpc.Core;

namespace Client {
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

    static readonly grpc::Marshaller<global::Client.AddPostsRequest> __Marshaller_posts_AddPostsRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Client.AddPostsRequest.Parser));
    static readonly grpc::Marshaller<global::Client.AddPostsReply> __Marshaller_posts_AddPostsReply = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Client.AddPostsReply.Parser));
    static readonly grpc::Marshaller<global::Client.DeletePostRequest> __Marshaller_posts_DeletePostRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Client.DeletePostRequest.Parser));
    static readonly grpc::Marshaller<global::Client.DeletePostReply> __Marshaller_posts_DeletePostReply = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Client.DeletePostReply.Parser));
    static readonly grpc::Marshaller<global::Client.ModifyPostRequest> __Marshaller_posts_ModifyPostRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Client.ModifyPostRequest.Parser));
    static readonly grpc::Marshaller<global::Client.ModifyPostReply> __Marshaller_posts_ModifyPostReply = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Client.ModifyPostReply.Parser));

    static readonly grpc::Method<global::Client.AddPostsRequest, global::Client.AddPostsReply> __Method_AddPost = new grpc::Method<global::Client.AddPostsRequest, global::Client.AddPostsReply>(
        grpc::MethodType.Unary,
        __ServiceName,
        "AddPost",
        __Marshaller_posts_AddPostsRequest,
        __Marshaller_posts_AddPostsReply);

    static readonly grpc::Method<global::Client.DeletePostRequest, global::Client.DeletePostReply> __Method_DeletePost = new grpc::Method<global::Client.DeletePostRequest, global::Client.DeletePostReply>(
        grpc::MethodType.Unary,
        __ServiceName,
        "DeletePost",
        __Marshaller_posts_DeletePostRequest,
        __Marshaller_posts_DeletePostReply);

    static readonly grpc::Method<global::Client.ModifyPostRequest, global::Client.ModifyPostReply> __Method_ModifyPost = new grpc::Method<global::Client.ModifyPostRequest, global::Client.ModifyPostReply>(
        grpc::MethodType.Unary,
        __ServiceName,
        "ModifyPost",
        __Marshaller_posts_ModifyPostRequest,
        __Marshaller_posts_ModifyPostReply);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::Client.PostsReflection.Descriptor.Services[0]; }
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

      public virtual global::Client.AddPostsReply AddPost(global::Client.AddPostsRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return AddPost(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::Client.AddPostsReply AddPost(global::Client.AddPostsRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_AddPost, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::Client.AddPostsReply> AddPostAsync(global::Client.AddPostsRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return AddPostAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::Client.AddPostsReply> AddPostAsync(global::Client.AddPostsRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_AddPost, null, options, request);
      }
      public virtual global::Client.DeletePostReply DeletePost(global::Client.DeletePostRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return DeletePost(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::Client.DeletePostReply DeletePost(global::Client.DeletePostRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_DeletePost, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::Client.DeletePostReply> DeletePostAsync(global::Client.DeletePostRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return DeletePostAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::Client.DeletePostReply> DeletePostAsync(global::Client.DeletePostRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_DeletePost, null, options, request);
      }
      public virtual global::Client.ModifyPostReply ModifyPost(global::Client.ModifyPostRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return ModifyPost(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::Client.ModifyPostReply ModifyPost(global::Client.ModifyPostRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_ModifyPost, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::Client.ModifyPostReply> ModifyPostAsync(global::Client.ModifyPostRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return ModifyPostAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::Client.ModifyPostReply> ModifyPostAsync(global::Client.ModifyPostRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_ModifyPost, null, options, request);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      protected override PostGrpcClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new PostGrpcClient(configuration);
      }
    }

  }
}
#endregion
