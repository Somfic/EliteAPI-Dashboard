import { get, Writable, writable } from "svelte/store";
import type { Exception } from "./error";
import type { Paths } from "./paths";

const port = 51555;
let socket: WebSocket | null = null;

export let isConnected = writable(false);
export let error: Writable<Exception | undefined> = writable(undefined);
export let events: Writable<Paths[]> = writable([]);

export function send(type: string, payload: any) {
    if (!isConnected) {
        console.warn("Cannot send WebSocket message, not connected");
        return;
    }

    socket.send(JSON.stringify({ type: type, payload: payload }));
}

let id = 0;

export function connect() {
    if (get(isConnected)) return;

    socket = new WebSocket(`ws://localhost:${port}`);
    socket.onopen = () => {
        console.log("connected");
        setTimeout(() => {
            isConnected.set(true);
        }, 100); // Allow for error message to come in
    };

    socket.onmessage = (message) => {
        let data = JSON.parse(message.data);

        console.log(message);

        switch (data["Type"]) {
            case "Error":
                error.set(data.Payload.exception as Exception);
                break;

            case "Paths":
                let rawPaths = data.Payload as Paths;
                let paths = parsePaths(rawPaths);

                events.update((items) => {
                    items.unshift(paths);
                    return items;
                });

                break;

            case "Backlog":
                let rawBacklog = data.Payload.filter((x: any) => x.paths) as Paths[];
                let backlog = [];

                rawBacklog.reverse();
                rawBacklog.forEach((rawPath) => backlog.push(parsePaths(rawPath)));

                events.set([...backlog, ...get(events)]);
                break;

            default:
                console.warn("Unknown message type", data);
        }
    };

    socket.onerror = (error) => {
        console.log("WebSocket error", error);
    };

    socket.onclose = () => {
        isConnected.set(false);

        setTimeout(function () {
            connect();
        }, 1000);
    };
}

function parsePaths(raw: Paths): Paths {
    let paths = raw;

    paths.timestamp = new Date();

    paths.event = JSON.parse(paths.paths.find((x) => x.Path.endsWith("Event")).Value);

    paths.paths.forEach((path) => {
        path.Parts = path.Path.split(".");
        path.Value = path.Value == null || path.Value == "" ? "" : JSON.parse(path.Value);
        path.ValueType = typeof path.Value;
    });

    paths.id = id++;

    return paths;
}
