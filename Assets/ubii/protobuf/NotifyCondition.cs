// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: proto/conditions/notifyCondition.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Ubii.Conditions {

  /// <summary>Holder for reflection information generated from proto/conditions/notifyCondition.proto</summary>
  public static partial class NotifyConditionReflection {

    #region Descriptor
    /// <summary>File descriptor for proto/conditions/notifyCondition.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static NotifyConditionReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "CiZwcm90by9jb25kaXRpb25zL25vdGlmeUNvbmRpdGlvbi5wcm90bxIPdWJp",
            "aS5jb25kaXRpb25zGhpwcm90by9jbGllbnRzL2NsaWVudC5wcm90byK4AQoP",
            "Tm90aWZ5Q29uZGl0aW9uEgoKAmlkGAEgASgJEgwKBG5hbWUYAiABKAkSJwof",
            "ZXZhbHVhdGlvbl9mdW5jdGlvbl9zdHJpbmdpZmllZBgDIAEoCRIwChJjbGll",
            "bnRfcHJvZmlsZV9wdWIYBCABKAsyFC51YmlpLmNsaWVudHMuQ2xpZW50EjAK",
            "EmNsaWVudF9wcm9maWxlX3N1YhgFIAEoCzIULnViaWkuY2xpZW50cy5DbGll",
            "bnRiBnByb3RvMw=="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::Ubii.Clients.ClientReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Ubii.Conditions.NotifyCondition), global::Ubii.Conditions.NotifyCondition.Parser, new[]{ "Id", "Name", "EvaluationFunctionStringified", "ClientProfilePub", "ClientProfileSub" }, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class NotifyCondition : pb::IMessage<NotifyCondition> {
    private static readonly pb::MessageParser<NotifyCondition> _parser = new pb::MessageParser<NotifyCondition>(() => new NotifyCondition());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<NotifyCondition> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Ubii.Conditions.NotifyConditionReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public NotifyCondition() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public NotifyCondition(NotifyCondition other) : this() {
      id_ = other.id_;
      name_ = other.name_;
      evaluationFunctionStringified_ = other.evaluationFunctionStringified_;
      clientProfilePub_ = other.clientProfilePub_ != null ? other.clientProfilePub_.Clone() : null;
      clientProfileSub_ = other.clientProfileSub_ != null ? other.clientProfileSub_.Clone() : null;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public NotifyCondition Clone() {
      return new NotifyCondition(this);
    }

    /// <summary>Field number for the "id" field.</summary>
    public const int IdFieldNumber = 1;
    private string id_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Id {
      get { return id_; }
      set {
        id_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "name" field.</summary>
    public const int NameFieldNumber = 2;
    private string name_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Name {
      get { return name_; }
      set {
        name_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "evaluation_function_stringified" field.</summary>
    public const int EvaluationFunctionStringifiedFieldNumber = 3;
    private string evaluationFunctionStringified_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string EvaluationFunctionStringified {
      get { return evaluationFunctionStringified_; }
      set {
        evaluationFunctionStringified_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "client_profile_pub" field.</summary>
    public const int ClientProfilePubFieldNumber = 4;
    private global::Ubii.Clients.Client clientProfilePub_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Ubii.Clients.Client ClientProfilePub {
      get { return clientProfilePub_; }
      set {
        clientProfilePub_ = value;
      }
    }

    /// <summary>Field number for the "client_profile_sub" field.</summary>
    public const int ClientProfileSubFieldNumber = 5;
    private global::Ubii.Clients.Client clientProfileSub_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Ubii.Clients.Client ClientProfileSub {
      get { return clientProfileSub_; }
      set {
        clientProfileSub_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as NotifyCondition);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(NotifyCondition other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Id != other.Id) return false;
      if (Name != other.Name) return false;
      if (EvaluationFunctionStringified != other.EvaluationFunctionStringified) return false;
      if (!object.Equals(ClientProfilePub, other.ClientProfilePub)) return false;
      if (!object.Equals(ClientProfileSub, other.ClientProfileSub)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Id.Length != 0) hash ^= Id.GetHashCode();
      if (Name.Length != 0) hash ^= Name.GetHashCode();
      if (EvaluationFunctionStringified.Length != 0) hash ^= EvaluationFunctionStringified.GetHashCode();
      if (clientProfilePub_ != null) hash ^= ClientProfilePub.GetHashCode();
      if (clientProfileSub_ != null) hash ^= ClientProfileSub.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Id.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(Id);
      }
      if (Name.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(Name);
      }
      if (EvaluationFunctionStringified.Length != 0) {
        output.WriteRawTag(26);
        output.WriteString(EvaluationFunctionStringified);
      }
      if (clientProfilePub_ != null) {
        output.WriteRawTag(34);
        output.WriteMessage(ClientProfilePub);
      }
      if (clientProfileSub_ != null) {
        output.WriteRawTag(42);
        output.WriteMessage(ClientProfileSub);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Id.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Id);
      }
      if (Name.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Name);
      }
      if (EvaluationFunctionStringified.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(EvaluationFunctionStringified);
      }
      if (clientProfilePub_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(ClientProfilePub);
      }
      if (clientProfileSub_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(ClientProfileSub);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(NotifyCondition other) {
      if (other == null) {
        return;
      }
      if (other.Id.Length != 0) {
        Id = other.Id;
      }
      if (other.Name.Length != 0) {
        Name = other.Name;
      }
      if (other.EvaluationFunctionStringified.Length != 0) {
        EvaluationFunctionStringified = other.EvaluationFunctionStringified;
      }
      if (other.clientProfilePub_ != null) {
        if (clientProfilePub_ == null) {
          clientProfilePub_ = new global::Ubii.Clients.Client();
        }
        ClientProfilePub.MergeFrom(other.ClientProfilePub);
      }
      if (other.clientProfileSub_ != null) {
        if (clientProfileSub_ == null) {
          clientProfileSub_ = new global::Ubii.Clients.Client();
        }
        ClientProfileSub.MergeFrom(other.ClientProfileSub);
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            Id = input.ReadString();
            break;
          }
          case 18: {
            Name = input.ReadString();
            break;
          }
          case 26: {
            EvaluationFunctionStringified = input.ReadString();
            break;
          }
          case 34: {
            if (clientProfilePub_ == null) {
              clientProfilePub_ = new global::Ubii.Clients.Client();
            }
            input.ReadMessage(clientProfilePub_);
            break;
          }
          case 42: {
            if (clientProfileSub_ == null) {
              clientProfileSub_ = new global::Ubii.Clients.Client();
            }
            input.ReadMessage(clientProfileSub_);
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code