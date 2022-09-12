<script lang="ts">
    import { Paths } from "../api/paths";

    import { flip } from "svelte/animate";
    export let events: Paths[];

    function parse(json: string) {
        if (json == null || json == "") return "";

        try {
            return JSON.parse(json);
        } catch (e) {
            return json;
        }
    }
</script>

<div class="events">
    {#each events as event (event.id)}
        <div class="event" animate:flip={{ duration: 300 }}>
            <p class="title">{JSON.parse(event.paths.find((x) => x.Path.endsWith("Event")).Value)}</p>
            <div class="paths">
                {#each event.paths.map((x) => ({ path: x.Path, value: parse(x.Value), parts: x.Path.split(".") })) as path}
                    <div class="path">
                        <div class="path-segments">
                            {#each path.parts as segment, index}
                                <div class="segment">{segment}</div>
                                {#if index < path.parts.length - 1}
                                    <div class="separator">.</div>
                                {/if}
                            {/each}
                        </div>
                        <div class={"value type-" + typeof path.value}>{path.value}</div>
                    </div>
                {/each}
            </div>
        </div>
    {/each}
</div>

<style lang="scss">
    @import "../styles/theme.scss";

    .events {
        flex-grow: 1;
        display: flex;
        flex-direction: column;
        overflow-x: hidden;
    }

    .event {
        background-color: $background-light;
        padding: 0.5rem 1rem;
        border-radius: 0.5rem;
        border: 1px solid $border;
        margin: 1rem;

        .title {
            font-size: 1.5rem;
            font-weight: 600;
        }

        .paths {
            display: flex;
            flex-direction: column;

            .path {
                display: flex;
                align-items: center;
                justify-content: space-between;

                .path-segments {
                    display: flex;
                    align-items: center;

                    .segment {
                        &:first-child {
                            color: $foreground-muted;
                        }
                    }

                    .separator {
                        color: $foreground-muted;
                    }
                }

                .value {
                    font-weight: 600;

                    &.type-string {
                        color: $string;
                    }

                    &.type-number {
                        color: $number;
                    }

                    &.type-boolean {
                        color: $boolean;
                    }
                }
            }
        }
    }
</style>
