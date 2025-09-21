import { apiConnector } from "../../../utils/apiConnector.js";
import { METHOD } from "../../../constants/api.js";

const BASE_API = '/api/laptops';

const endPoints = {
    LIST: BASE_API,
    DETAIL: (id) => `${BASE_API}/${id}`,
    CREATE: BASE_API,
    DELETE: (id) => `${BASE_API}/${id}`,
    EDIT: (id) => `${BASE_API}/${id}`,
};
export async function fetchLaptops() {
    const res = await apiConnector(METHOD.GET, endPoints.LIST);
    if (!res.data) throw new Error(`Failed to fetch laptops: ${res.status}`);
    return res.data;
}

export async function fetchLaptop(id) {
    const res = await apiConnector(METHOD.GET, endPoints.DETAIL(id));
    if (!res.data) throw new Error(`Failed to fetch laptop ${id}: ${res.status}`);
    return res.data;
}

export async function createLaptop(data) {
    const res = await apiConnector(METHOD.POST, endPoints.CREATE, data);
    if (!res.data) throw new Error(`Failed to create laptop: ${res.status}`);
    return res.data;
}

export async function updateLaptop(id, data) {
    const res = await apiConnector(METHOD.PUT, endPoints.EDIT(id), data);
    if (res.status !== 204) throw new Error(`Failed to update laptop ${id}: ${res.status}`);
}

// New: delete a laptop
export async function deleteLaptop(id) {
    const res = await apiConnector(METHOD.DELETE, endPoints.DELETE(id));
    if (res.status !== 204) throw new Error(`Failed to delete laptop ${id}: ${res.status}`);
}