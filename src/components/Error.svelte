<script lang="ts">
    import type { Exception } from "../api/error";

    export let error: Exception;
</script>

<div class="error-prompt">
    <div class="error">
        <p class="type">{error.ClassName}</p>
        <p class="message">EliteAPI could not be started</p>
        <p class="exception">{error.Message}</p>
        <p class="data">{JSON.stringify(error.Data, null, "\n")}</p>
        <div class="contact">
            <p>Need help?</p>
            <a href="https://discord.com/invite/jwpFUPZ" target="_blank">Join the Discord</a>
        </div>
    </div>
</div>

<style lang="scss">
    @import "../styles/theme";

    .error-prompt {
        flex-grow: 1;
        display: flex;
        align-items: center;
        justify-content: center;
        padding: 1rem;

        .error {
            padding: 2rem;
            border: 2px solid $error;
            border-radius: 1rem;
            background-color: $background-dark;

            .type {
                display: inline-block;
                font-family: monospace;
                font-size: 1.25rem;
                background-color: $background-light;
                border-radius: 0.5rem;
                padding: 0.25rem 0.75rem;
            }

            .message {
                font-size: 3rem;
                font-weight: 600;
                margin: 1rem 0;
                padding: 0.25rem;
            }

            .exception {
                font-size: 1.5rem;
                padding: 0.25rem;
                margin: 1rem 0;
            }

            .data {
                font-family: monospace;
                background-color: $background-light;
                border-radius: 0.5rem;
                padding: 0.6rem 0.75rem;
            }

            .contact {
                margin-top: 2rem;

                display: flex;
                align-items: center;
                justify-content: right;
                color: $foreground-muted;

                p {
                    margin-right: 1rem;
                }

                a {
                    margin: 0;
                    padding: 0.5rem 1rem;
                    border: 2px solid $border;
                    border-radius: 0.5rem;
                    background-color: $background-dark;
                    color: $foreground-muted;
                    transition: 200ms ease;
                    text-decoration: none;

                    &:hover {
                        background-color: $background-light;
                        border-color: $foreground-muted;
                        cursor: pointer;
                    }

                    &:active {
                        border-color: $foreground;
                        background-color: $background-dark;
                        scale: 0.98;
                    }
                }
            }
        }
    }
</style>
