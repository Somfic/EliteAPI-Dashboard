<script lang="ts">
	export let progress: number;
</script>

<div class="progress-bar">
	<div class="progress" style="width: {progress}%" />
</div>

<style lang="scss">
	@import '../styles/theme.scss';

	.progress-bar {
		width: 100%;
		height: 1px;
		background-color: $backdrop;
		border-radius: 10px;
	}

	.progress {
		height: 100%;
		background-color: $accent;
		border-radius: 10px;
		transition: width 0.5s ease;
	}
</style>
