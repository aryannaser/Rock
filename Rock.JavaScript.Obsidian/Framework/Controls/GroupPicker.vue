<template>
    <!-- We use BaseAsyncPicker, passing down standard props and our computed items list -->
    <BaseAsyncPicker v-model="internalValue"
                     v-bind="standardProps"
                     :items="actualItems" />
</template>

<script setup lang="ts">
    import { standardAsyncPickerProps, useStandardAsyncPickerProps, useVModelPassthrough } from "@Obsidian/Utility/component";
    import { useHttp } from "@Obsidian/Utility/http";
    import { ListItemBag } from "@Obsidian/ViewModels/Utility/listItemBag";
    import { computed, PropType, ref, watch } from "vue";
    import BaseAsyncPicker from "./baseAsyncPicker.obs";
    import { Guid } from "@Obsidian/Types";

    const props = defineProps({
        modelValue: {
            type: String as PropType<Guid | null>,
            required: false
        },

        /**
         * Determines if inactive groups should be included in the list.
         * (Renamed per boolean guidelines to reflect Is/Has prefix.)
         */
        isInactiveIncluded: {
            type: Boolean as PropType<boolean>,
            default: false
        },

        // This pulls in all the standard picker properties like 'label', 'multiple', 'help', etc.
        ...standardAsyncPickerProps
    });

    const emit = defineEmits<{
        (e: "update:modelValue", value: string | null): void;
    }>();

    // These are helpers from Rock's component utilities.
    const internalValue = useVModelPassthrough(props, "modelValue", emit);
    const standardProps = useStandardAsyncPickerProps(props);
    const http = useHttp();

    // This ref will store the items *after* they are loaded from the API once.
    const loadedItems = ref<ListItemBag[] | null>(null);

    /**
     * This computed property intelligently provides either the already-loaded items
     * or the function to load them. This prevents re-fetching data every time.
     */
    const actualItems = computed((): ListItemBag[] | (() => Promise<ListItemBag[]>) => {
        // If we have already loaded the items, return them. Otherwise, return the function to load them.
        return loadedItems.value || loadOptions;
    });

    /**
     * Loads the group options from our custom API endpoint.
     */
    const loadOptions = async (): Promise<ListItemBag[]> => {
        const apiUrl = `/api/v2/controls/grouppickerdata?includeInactive=${props.isInactiveIncluded}`;
        const result = await http.get<ListItemBag[]>(apiUrl);

        if (result.isSuccess && result.data) {
            loadedItems.value = result.data; // Store the items for next time.
            return result.data;
        }
        else {
            console.error(result.errorMessage ?? "Unknown error while loading group data.");
            loadedItems.value = []; // Store an empty array on failure.
            return [];
        }
    };

    /**
     * This is the key to making the 'includeInactive' prop work.
     * We "watch" the prop for changes. If it ever changes, we clear our
     * cached 'loadedItems', which forces the component to re-fetch.
     */
    watch(() => props.isInactiveIncluded, () => {
        loadedItems.value = null;
    });
</script>