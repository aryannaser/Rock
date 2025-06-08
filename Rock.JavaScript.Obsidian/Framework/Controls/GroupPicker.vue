<template>
    <!-- We are now using BaseAsyncPicker instead of ValuePicker -->
    <BaseAsyncPicker v-model="internalValue" :loadItems="loadItems" label="Group" />
</template>

<script setup lang="ts">
    import { useVModel } from "@vueuse/core";
    import { PropType } from "vue";
    import BaseAsyncPicker from "./BaseAsyncPicker.vue"; // The correct base component
    import { useHttp } from "@Obsidian/Utility/http";
    import { Guid } from "@Obsidian/Types";
    import { ListItemBag } from "@Obsidian/ViewModels/Utility/listItemBag";

    const props = defineProps({
        modelValue: {
            type: String as PropType<Guid | null>,
            required: false,
        }
    });

    const emit = defineEmits<{
        (e: "update:modelValue", value: string | null): void;
    }>();

    const http = useHttp();
    const internalValue = useVModel(props, "modelValue", emit);

    /**
     * This is the function that BaseAsyncPicker will call to get its data.
     * It just needs to fetch the data and return it.
     */
    async function loadItems(): Promise<ListItemBag[]> {
        const result = await http.get<ListItemBag[]>("/api/v2/controls/grouppickerdata");

        if (result.isSuccess && result.data) {
            // The BaseAsyncPicker expects an array of ListItemBag, which has 'value' and 'text' properties.
            // Our API and ViewModel already provide this, so we just return the data.
            return result.data;
        }
        else {
            console.error("SimpleGroupPicker failed to load items:", result.errorMessage ?? "Unknown error");
            return [];
        }
    }
</script>