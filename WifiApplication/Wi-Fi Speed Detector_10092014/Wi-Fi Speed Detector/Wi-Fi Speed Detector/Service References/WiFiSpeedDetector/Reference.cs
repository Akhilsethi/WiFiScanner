﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.6387
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Wi_Fi_Speed_Detector.WiFiSpeedDetector {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="WiFiSpeedDetector.Service1Soap")]
    public interface Service1Soap {
        
        // CODEGEN: Generating message contract since element name HelloWorldResult from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/HelloWorld", ReplyAction="*")]
        Wi_Fi_Speed_Detector.WiFiSpeedDetector.HelloWorldResponse HelloWorld(Wi_Fi_Speed_Detector.WiFiSpeedDetector.HelloWorldRequest request);
        
        // CODEGEN: Generating message contract since element name oData from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/UploadGlobalFile", ReplyAction="*")]
        Wi_Fi_Speed_Detector.WiFiSpeedDetector.UploadGlobalFileResponse UploadGlobalFile(Wi_Fi_Speed_Detector.WiFiSpeedDetector.UploadGlobalFileRequest request);
        
        // CODEGEN: Generating message contract since element name oData from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/UploadFiles", ReplyAction="*")]
        Wi_Fi_Speed_Detector.WiFiSpeedDetector.UploadFilesResponse UploadFiles(Wi_Fi_Speed_Detector.WiFiSpeedDetector.UploadFilesRequest request);
        
        // CODEGEN: Generating message contract since element name fileData from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/UploadTextFile", ReplyAction="*")]
        Wi_Fi_Speed_Detector.WiFiSpeedDetector.UploadTextFileResponse UploadTextFile(Wi_Fi_Speed_Detector.WiFiSpeedDetector.UploadTextFileRequest request);
        
        // CODEGEN: Generating message contract since element name DownloadTextFileResult from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/DownloadTextFile", ReplyAction="*")]
        Wi_Fi_Speed_Detector.WiFiSpeedDetector.DownloadTextFileResponse DownloadTextFile(Wi_Fi_Speed_Detector.WiFiSpeedDetector.DownloadTextFileRequest request);
        
        // CODEGEN: Generating message contract since element name DownloadFileResult from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/DownloadFile", ReplyAction="*")]
        Wi_Fi_Speed_Detector.WiFiSpeedDetector.DownloadFileResponse DownloadFile(Wi_Fi_Speed_Detector.WiFiSpeedDetector.DownloadFileRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class HelloWorldRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="HelloWorld", Namespace="http://tempuri.org/", Order=0)]
        public Wi_Fi_Speed_Detector.WiFiSpeedDetector.HelloWorldRequestBody Body;
        
        public HelloWorldRequest() {
        }
        
        public HelloWorldRequest(Wi_Fi_Speed_Detector.WiFiSpeedDetector.HelloWorldRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute()]
    public partial class HelloWorldRequestBody {
        
        public HelloWorldRequestBody() {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class HelloWorldResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="HelloWorldResponse", Namespace="http://tempuri.org/", Order=0)]
        public Wi_Fi_Speed_Detector.WiFiSpeedDetector.HelloWorldResponseBody Body;
        
        public HelloWorldResponse() {
        }
        
        public HelloWorldResponse(Wi_Fi_Speed_Detector.WiFiSpeedDetector.HelloWorldResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class HelloWorldResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string HelloWorldResult;
        
        public HelloWorldResponseBody() {
        }
        
        public HelloWorldResponseBody(string HelloWorldResult) {
            this.HelloWorldResult = HelloWorldResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class UploadGlobalFileRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="UploadGlobalFile", Namespace="http://tempuri.org/", Order=0)]
        public Wi_Fi_Speed_Detector.WiFiSpeedDetector.UploadGlobalFileRequestBody Body;
        
        public UploadGlobalFileRequest() {
        }
        
        public UploadGlobalFileRequest(Wi_Fi_Speed_Detector.WiFiSpeedDetector.UploadGlobalFileRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class UploadGlobalFileRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public byte[] oData;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string sfile;
        
        public UploadGlobalFileRequestBody() {
        }
        
        public UploadGlobalFileRequestBody(byte[] oData, string sfile) {
            this.oData = oData;
            this.sfile = sfile;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class UploadGlobalFileResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="UploadGlobalFileResponse", Namespace="http://tempuri.org/", Order=0)]
        public Wi_Fi_Speed_Detector.WiFiSpeedDetector.UploadGlobalFileResponseBody Body;
        
        public UploadGlobalFileResponse() {
        }
        
        public UploadGlobalFileResponse(Wi_Fi_Speed_Detector.WiFiSpeedDetector.UploadGlobalFileResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class UploadGlobalFileResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=0)]
        public int UploadGlobalFileResult;
        
        public UploadGlobalFileResponseBody() {
        }
        
        public UploadGlobalFileResponseBody(int UploadGlobalFileResult) {
            this.UploadGlobalFileResult = UploadGlobalFileResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class UploadFilesRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="UploadFiles", Namespace="http://tempuri.org/", Order=0)]
        public Wi_Fi_Speed_Detector.WiFiSpeedDetector.UploadFilesRequestBody Body;
        
        public UploadFilesRequest() {
        }
        
        public UploadFilesRequest(Wi_Fi_Speed_Detector.WiFiSpeedDetector.UploadFilesRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class UploadFilesRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public byte[] oData;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string sfile;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=2)]
        public int iGlobalID;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=3)]
        public int iOption;
        
        public UploadFilesRequestBody() {
        }
        
        public UploadFilesRequestBody(byte[] oData, string sfile, int iGlobalID, int iOption) {
            this.oData = oData;
            this.sfile = sfile;
            this.iGlobalID = iGlobalID;
            this.iOption = iOption;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class UploadFilesResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="UploadFilesResponse", Namespace="http://tempuri.org/", Order=0)]
        public Wi_Fi_Speed_Detector.WiFiSpeedDetector.UploadFilesResponseBody Body;
        
        public UploadFilesResponse() {
        }
        
        public UploadFilesResponse(Wi_Fi_Speed_Detector.WiFiSpeedDetector.UploadFilesResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute()]
    public partial class UploadFilesResponseBody {
        
        public UploadFilesResponseBody() {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class UploadTextFileRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="UploadTextFile", Namespace="http://tempuri.org/", Order=0)]
        public Wi_Fi_Speed_Detector.WiFiSpeedDetector.UploadTextFileRequestBody Body;
        
        public UploadTextFileRequest() {
        }
        
        public UploadTextFileRequest(Wi_Fi_Speed_Detector.WiFiSpeedDetector.UploadTextFileRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class UploadTextFileRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public byte[] fileData;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string fileName;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=2)]
        public int option;
        
        public UploadTextFileRequestBody() {
        }
        
        public UploadTextFileRequestBody(byte[] fileData, string fileName, int option) {
            this.fileData = fileData;
            this.fileName = fileName;
            this.option = option;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class UploadTextFileResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="UploadTextFileResponse", Namespace="http://tempuri.org/", Order=0)]
        public Wi_Fi_Speed_Detector.WiFiSpeedDetector.UploadTextFileResponseBody Body;
        
        public UploadTextFileResponse() {
        }
        
        public UploadTextFileResponse(Wi_Fi_Speed_Detector.WiFiSpeedDetector.UploadTextFileResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class UploadTextFileResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=0)]
        public int UploadTextFileResult;
        
        public UploadTextFileResponseBody() {
        }
        
        public UploadTextFileResponseBody(int UploadTextFileResult) {
            this.UploadTextFileResult = UploadTextFileResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class DownloadTextFileRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="DownloadTextFile", Namespace="http://tempuri.org/", Order=0)]
        public Wi_Fi_Speed_Detector.WiFiSpeedDetector.DownloadTextFileRequestBody Body;
        
        public DownloadTextFileRequest() {
        }
        
        public DownloadTextFileRequest(Wi_Fi_Speed_Detector.WiFiSpeedDetector.DownloadTextFileRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute()]
    public partial class DownloadTextFileRequestBody {
        
        public DownloadTextFileRequestBody() {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class DownloadTextFileResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="DownloadTextFileResponse", Namespace="http://tempuri.org/", Order=0)]
        public Wi_Fi_Speed_Detector.WiFiSpeedDetector.DownloadTextFileResponseBody Body;
        
        public DownloadTextFileResponse() {
        }
        
        public DownloadTextFileResponse(Wi_Fi_Speed_Detector.WiFiSpeedDetector.DownloadTextFileResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class DownloadTextFileResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public byte[] DownloadTextFileResult;
        
        public DownloadTextFileResponseBody() {
        }
        
        public DownloadTextFileResponseBody(byte[] DownloadTextFileResult) {
            this.DownloadTextFileResult = DownloadTextFileResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class DownloadFileRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="DownloadFile", Namespace="http://tempuri.org/", Order=0)]
        public Wi_Fi_Speed_Detector.WiFiSpeedDetector.DownloadFileRequestBody Body;
        
        public DownloadFileRequest() {
        }
        
        public DownloadFileRequest(Wi_Fi_Speed_Detector.WiFiSpeedDetector.DownloadFileRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute()]
    public partial class DownloadFileRequestBody {
        
        public DownloadFileRequestBody() {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class DownloadFileResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="DownloadFileResponse", Namespace="http://tempuri.org/", Order=0)]
        public Wi_Fi_Speed_Detector.WiFiSpeedDetector.DownloadFileResponseBody Body;
        
        public DownloadFileResponse() {
        }
        
        public DownloadFileResponse(Wi_Fi_Speed_Detector.WiFiSpeedDetector.DownloadFileResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class DownloadFileResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public byte[] DownloadFileResult;
        
        public DownloadFileResponseBody() {
        }
        
        public DownloadFileResponseBody(byte[] DownloadFileResult) {
            this.DownloadFileResult = DownloadFileResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public interface Service1SoapChannel : Wi_Fi_Speed_Detector.WiFiSpeedDetector.Service1Soap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public partial class Service1SoapClient : System.ServiceModel.ClientBase<Wi_Fi_Speed_Detector.WiFiSpeedDetector.Service1Soap>, Wi_Fi_Speed_Detector.WiFiSpeedDetector.Service1Soap {
        
        public Service1SoapClient() {
        }
        
        public Service1SoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public Service1SoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service1SoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service1SoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Wi_Fi_Speed_Detector.WiFiSpeedDetector.HelloWorldResponse Wi_Fi_Speed_Detector.WiFiSpeedDetector.Service1Soap.HelloWorld(Wi_Fi_Speed_Detector.WiFiSpeedDetector.HelloWorldRequest request) {
            return base.Channel.HelloWorld(request);
        }
        
        public string HelloWorld() {
            Wi_Fi_Speed_Detector.WiFiSpeedDetector.HelloWorldRequest inValue = new Wi_Fi_Speed_Detector.WiFiSpeedDetector.HelloWorldRequest();
            inValue.Body = new Wi_Fi_Speed_Detector.WiFiSpeedDetector.HelloWorldRequestBody();
            Wi_Fi_Speed_Detector.WiFiSpeedDetector.HelloWorldResponse retVal = ((Wi_Fi_Speed_Detector.WiFiSpeedDetector.Service1Soap)(this)).HelloWorld(inValue);
            return retVal.Body.HelloWorldResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Wi_Fi_Speed_Detector.WiFiSpeedDetector.UploadGlobalFileResponse Wi_Fi_Speed_Detector.WiFiSpeedDetector.Service1Soap.UploadGlobalFile(Wi_Fi_Speed_Detector.WiFiSpeedDetector.UploadGlobalFileRequest request) {
            return base.Channel.UploadGlobalFile(request);
        }
        
        public int UploadGlobalFile(byte[] oData, string sfile) {
            Wi_Fi_Speed_Detector.WiFiSpeedDetector.UploadGlobalFileRequest inValue = new Wi_Fi_Speed_Detector.WiFiSpeedDetector.UploadGlobalFileRequest();
            inValue.Body = new Wi_Fi_Speed_Detector.WiFiSpeedDetector.UploadGlobalFileRequestBody();
            inValue.Body.oData = oData;
            inValue.Body.sfile = sfile;
            Wi_Fi_Speed_Detector.WiFiSpeedDetector.UploadGlobalFileResponse retVal = ((Wi_Fi_Speed_Detector.WiFiSpeedDetector.Service1Soap)(this)).UploadGlobalFile(inValue);
            return retVal.Body.UploadGlobalFileResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Wi_Fi_Speed_Detector.WiFiSpeedDetector.UploadFilesResponse Wi_Fi_Speed_Detector.WiFiSpeedDetector.Service1Soap.UploadFiles(Wi_Fi_Speed_Detector.WiFiSpeedDetector.UploadFilesRequest request) {
            return base.Channel.UploadFiles(request);
        }
        
        public void UploadFiles(byte[] oData, string sfile, int iGlobalID, int iOption) {
            Wi_Fi_Speed_Detector.WiFiSpeedDetector.UploadFilesRequest inValue = new Wi_Fi_Speed_Detector.WiFiSpeedDetector.UploadFilesRequest();
            inValue.Body = new Wi_Fi_Speed_Detector.WiFiSpeedDetector.UploadFilesRequestBody();
            inValue.Body.oData = oData;
            inValue.Body.sfile = sfile;
            inValue.Body.iGlobalID = iGlobalID;
            inValue.Body.iOption = iOption;
            Wi_Fi_Speed_Detector.WiFiSpeedDetector.UploadFilesResponse retVal = ((Wi_Fi_Speed_Detector.WiFiSpeedDetector.Service1Soap)(this)).UploadFiles(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Wi_Fi_Speed_Detector.WiFiSpeedDetector.UploadTextFileResponse Wi_Fi_Speed_Detector.WiFiSpeedDetector.Service1Soap.UploadTextFile(Wi_Fi_Speed_Detector.WiFiSpeedDetector.UploadTextFileRequest request) {
            return base.Channel.UploadTextFile(request);
        }
        
        public int UploadTextFile(byte[] fileData, string fileName, int option) {
            Wi_Fi_Speed_Detector.WiFiSpeedDetector.UploadTextFileRequest inValue = new Wi_Fi_Speed_Detector.WiFiSpeedDetector.UploadTextFileRequest();
            inValue.Body = new Wi_Fi_Speed_Detector.WiFiSpeedDetector.UploadTextFileRequestBody();
            inValue.Body.fileData = fileData;
            inValue.Body.fileName = fileName;
            inValue.Body.option = option;
            Wi_Fi_Speed_Detector.WiFiSpeedDetector.UploadTextFileResponse retVal = ((Wi_Fi_Speed_Detector.WiFiSpeedDetector.Service1Soap)(this)).UploadTextFile(inValue);
            return retVal.Body.UploadTextFileResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Wi_Fi_Speed_Detector.WiFiSpeedDetector.DownloadTextFileResponse Wi_Fi_Speed_Detector.WiFiSpeedDetector.Service1Soap.DownloadTextFile(Wi_Fi_Speed_Detector.WiFiSpeedDetector.DownloadTextFileRequest request) {
            return base.Channel.DownloadTextFile(request);
        }
        
        public byte[] DownloadTextFile() {
            Wi_Fi_Speed_Detector.WiFiSpeedDetector.DownloadTextFileRequest inValue = new Wi_Fi_Speed_Detector.WiFiSpeedDetector.DownloadTextFileRequest();
            inValue.Body = new Wi_Fi_Speed_Detector.WiFiSpeedDetector.DownloadTextFileRequestBody();
            Wi_Fi_Speed_Detector.WiFiSpeedDetector.DownloadTextFileResponse retVal = ((Wi_Fi_Speed_Detector.WiFiSpeedDetector.Service1Soap)(this)).DownloadTextFile(inValue);
            return retVal.Body.DownloadTextFileResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Wi_Fi_Speed_Detector.WiFiSpeedDetector.DownloadFileResponse Wi_Fi_Speed_Detector.WiFiSpeedDetector.Service1Soap.DownloadFile(Wi_Fi_Speed_Detector.WiFiSpeedDetector.DownloadFileRequest request) {
            return base.Channel.DownloadFile(request);
        }
        
        public byte[] DownloadFile() {
            Wi_Fi_Speed_Detector.WiFiSpeedDetector.DownloadFileRequest inValue = new Wi_Fi_Speed_Detector.WiFiSpeedDetector.DownloadFileRequest();
            inValue.Body = new Wi_Fi_Speed_Detector.WiFiSpeedDetector.DownloadFileRequestBody();
            Wi_Fi_Speed_Detector.WiFiSpeedDetector.DownloadFileResponse retVal = ((Wi_Fi_Speed_Detector.WiFiSpeedDetector.Service1Soap)(this)).DownloadFile(inValue);
            return retVal.Body.DownloadFileResult;
        }
    }
}