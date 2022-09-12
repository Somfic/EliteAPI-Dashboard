export interface Path {
    Path: string;
    Value: string;
}

export interface Context {
    IsRaisedDuringCatchup: boolean;
    IsImplemented: boolean;
    SourceFile: string;
}

export interface Paths {
    paths: Path[];
    context: Context;
    id: number;
}
