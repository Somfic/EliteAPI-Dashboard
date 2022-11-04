<script lang="ts">
	import { onMount } from 'svelte';
	import { goto } from '$app/navigation';
	import { isConnected } from '../api/websocket';
	import { get } from 'svelte/store';

	onMount(() => {
		if (!get(isConnected)) {
			console.log('goto');
			goto('init');
		}
	});
</script>

<h1>main</h1>
