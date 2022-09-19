export interface Path {
    Path: string;
    Value: string;
    ValueType: string;
    Parts: string[];
}

export interface Context {
    IsRaisedDuringCatchup: boolean;
    IsImplemented: boolean;
    SourceFile: string;
}

export interface Paths {
    event: string;
    paths: Path[];
    context: Context;
    id: number;
    timestamp: Date;
}
