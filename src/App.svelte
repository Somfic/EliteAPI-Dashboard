<script lang="ts">
    import { onMount } from "svelte";
    import { connect, isConnected, error, events } from "./api/websocket";
    import Error from "./components/Error.svelte";
    import Events from "./components/Events.svelte";

    onMount(() => {
        connect();
    });
</script>

{#if $error != undefined}
    <Error error={$error} />
{:else if $isConnected}
    {#if $events.length == 0}
        <div class="connection-prompt">
            <h1>Loading</h1>
        </div>
    {:else}
        <Events events={$events} />
    {/if}
{:else}
    <div class="connection-prompt">
        <h1>Connecting</h1>
    </div>
{/if}

<style lang="scss">
    @import "./styles/theme";

    .connection-prompt {
        flex-grow: 1;
        align-items: center;
        justify-content: center;
    }
</style>
