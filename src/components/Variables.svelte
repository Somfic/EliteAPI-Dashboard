<script lang="ts">
    import type { Path } from "../api/paths";
    import fuzzysort from "fuzzysort";

    let search;

    export let variables: Path[];

    $: sortedVariables = fuzzysort.go(search, variables, {
        keys: ["Value", "Parts"],
        threshold: -10000,
    });
</script>

<input type="text" bind:value={search} placeholder="Search" />

{#each sortedVariables as variable}
    <p>{JSON.stringify(variable)}</p>
{/each}
