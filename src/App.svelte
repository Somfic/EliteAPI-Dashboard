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
    <Events events={$events} />
{:else}
    <div class="connection-prompt">
        <h1>Connecting</h1>
    </div>
{/if}

<style lang="scss">
    @import "./styles/theme";
</style>
