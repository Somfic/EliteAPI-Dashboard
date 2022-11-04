<script lang="ts">
	import ProgressBar from '../../components/ProgressBar.svelte';
	import { onMount } from 'svelte';
	import { connect, isConnected, error, events, variables } from '../../api/websocket';

	onMount(() => {
		connect();
	});
</script>

<main>
	<img src="eliteapi.png" alt="" />

	<div class="status">
		<h1>Initialising</h1>
		<h2>{$isConnected ? 'Connected with EliteAPI' : 'Connecting to EliteAPI'}</h2>
	</div>

	<div class="progress">
		<ProgressBar progress={10} />
	</div>
</main>

<style lang="scss">
	@import '../../styles/theme.scss';

	main {
		display: flex;
		flex-direction: column;
		align-items: center;
		justify-content: center;
		flex-grow: 1;
		gap: 50px;

		img {
			border-radius: 15px;
			width: 222px;
			height: 222px;
		}

		.status {
			display: flex;
			flex-direction: column;
			align-items: center;
			justify-content: center;
			gap: 5px;

			h1,
			h2 {
				margin: 0;
			}

			h1 {
				font-size: 40px;
				font-weight: bold;
			}

			h2 {
				font-size: 14px;
				color: $muted;
				font-weight: 400;
			}
		}

		.progress {
			max-width: 333px;
			width: 100%;
		}
	}
</style>
