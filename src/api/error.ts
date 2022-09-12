export interface Exception {
    ClassName: string;
    Message: string;
    Data: any[];
    InnerException?: any;
    HelpURL?: any;
    StackTraceString: string;
    RemoteStackTraceString?: any;
    RemoteStackIndex: number;
    ExceptionMethod?: any;
    HResult: number;
    Source: string;
    WatsonBuckets?: any;
}
